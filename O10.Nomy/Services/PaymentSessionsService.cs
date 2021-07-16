using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using O10.Core.Architecture;
using O10.Core.Cryptography;
using O10.Core.ExtensionMethods;
using O10.Core.HashCalculations;
using O10.Crypto.ConfidentialAssets;
using O10.Nomy.DTOs;
using O10.Nomy.Hubs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(IPaymentSessionsService), Lifetime = LifetimeManagement.Singleton)]
    public class PaymentSessionsService : IPaymentSessionsService
    {
        private readonly IHashCalculation _hashCalculation;
        private readonly Dictionary<string, PaymentSession> _paymentSessions = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataAccessService _dataAccessService;
        private readonly IHubContext<PaymentSessionHub> _hubContext;

        public PaymentSessionsService(IServiceProvider serviceProvider,
                                      IHashCalculationsRepository hashCalculationsRepository,
                                      IDataAccessService dataAccessService,
                                      IHubContext<PaymentSessionHub> hubContext)
        {
            _hashCalculation = hashCalculationsRepository.Create(HashType.Keccak256);
            _serviceProvider = serviceProvider;
            _dataAccessService = dataAccessService;
            _hubContext = hubContext;
        }

        public string CreatePaymentsSession()
        {
            string sessionId = Guid.NewGuid().ToString();

            _paymentSessions.Add(sessionId, new PaymentSession { SessionId = sessionId });

            return sessionId;
        }

        public async Task<PaymentSessionEntry?> PushInvoice(long userId, string sessionId, string currency, ulong amount, CancellationToken ct)
        {
            if(!_paymentSessions.ContainsKey(sessionId))
            {
                throw new ArgumentOutOfRangeException(nameof(sessionId));
            }

            var invoiceEntry = GenerateInvoiceEntry(sessionId, currency, amount);

            var record = _paymentSessions[sessionId].Records[invoiceEntry.Commitment];

            await _hubContext.Clients.Group($"{sessionId}_Payer").SendAsync("Invoice", invoiceEntry);

            record.TimeoutTask.Start();

            var invoiceRecord = await _dataAccessService.AddInvoiceRecord(userId,invoiceEntry.Commitment, JsonConvert.SerializeObject(invoiceEntry.RangeProof), ct);
            var secretInvoiceRecord = await _dataAccessService.AddSecretInvoiceRecord(userId, invoiceRecord.InvoiceRecordId, invoiceEntry.Mask, amount, ct);

            await record.TimeoutTask.ContinueWith(t =>
            {
                if(!t.IsCompletedSuccessfully)
                {
                    return;
                }

                lock(_paymentSessions)
                {
                    if (t.Result.TimeoutCancellation.IsCancellationRequested)
                    {
                        return;
                    }
                    _paymentSessions.Remove(t.Result.SessionId);
                }

                _hubContext.Clients.Group($"{sessionId}_Payee").SendAsync("Timeout");
            }, TaskScheduler.Default);

            return record.InvoiceEntry;
        }

        public async Task<PaymentSessionEntry?> PushPayment(long userId, string sessionId, string invoiceCommitment, string currency, ulong amount, CancellationToken ct)
        {
            lock(_paymentSessions)
            {
                if (!_paymentSessions.ContainsKey(sessionId))
                {
                    return null;
                }

                _paymentSessions[sessionId].Records[invoiceCommitment].TimeoutCancellation.Cancel();
            }

            var paymentEntry = GeneratePaymentEntry(sessionId, invoiceCommitment, currency, amount);

            byte[] diffCommitments = CryptoHelper.SubCommitments(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry.Commitment, _paymentSessions[sessionId].Records[invoiceCommitment].InvoiceEntry.Commitment);
            byte[] msg = _hashCalculation.CalculateHash(sessionId);
            bool resDiff = CryptoHelper.VerifyRingSignature(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentSignature, msg, new byte[][] { diffCommitments });

            if(resDiff)
            {
                var paymentRecord = await _dataAccessService.AddPaymentRecord(userId,
                                                                              _paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry.Commitment.ToHexString(),
                                                                              JsonConvert.SerializeObject(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry.RangeProof),
                                                                              JsonConvert.SerializeObject(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentSignature),
                                                                              _paymentSessions[sessionId].Records[invoiceCommitment].InvoiceEntry.Commitment.ToHexString(),
                                                                              ct);
                var secretPaymentRecord = await _dataAccessService.AddSecretPaymentRecord(userId, paymentRecord.PaymentRecordId, _paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry.Mask.ToHexString(), amount, ct);
                await _hubContext.Clients.Group($"{sessionId}_Payee").SendAsync("Payment", paymentEntry);
            }
            else
            {
                await _hubContext.Clients.Group($"{sessionId}_Payee").SendAsync("Error", paymentEntry);
            }

            return _paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry;
        }

        private PaymentEntryDTO GenerateInvoiceEntry(string sessionId, string currency, ulong amount)
        {
            byte[] currencyCommitment = CryptoHelper.GetNonblindedAssetCommitment(_hashCalculation.CalculateHash(currency));
            RangeProof rangeProof = CryptoHelper.ProveRange(out byte[] invoiceCommitment, out byte[] invoiceBlindingFactor, amount, currencyCommitment);

            // TODO: need to create a factory
            var record = ActivatorUtilities.CreateInstance<PaymentSessionRecord>(_serviceProvider);
            record.SessionId = sessionId;
            record.InvoiceEntry = new PaymentSessionEntry { Commitment = invoiceCommitment, Mask = invoiceBlindingFactor, RangeProof = rangeProof };

            _paymentSessions[sessionId].Records.Add(invoiceCommitment.ToHexString(), record);

            return new PaymentEntryDTO
            {
                SessionId = sessionId,
                Commitment = invoiceCommitment.ToHexString(),
                RangeProof = rangeProof,
                Mask = invoiceBlindingFactor.ToHexString()
            };
        }

        private PaymentEntryDTO GeneratePaymentEntry(string sessionId, string invoiceCommitment, string currency, ulong amount)
        {
            byte[] currencyCommitment = CryptoHelper.GetNonblindedAssetCommitment(_hashCalculation.CalculateHash(currency));
            RangeProof rangeProof = CryptoHelper.ProveRange(out byte[] paymentCommitment, out byte[] paymentBlindingFactor, amount, currencyCommitment);

            _paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry = 
                new PaymentSessionEntry { Commitment = paymentCommitment, Mask = paymentBlindingFactor, RangeProof = rangeProof };

            byte[] diffBF = CryptoHelper.GetDifferentialBlindingFactor(paymentBlindingFactor, _paymentSessions[sessionId].Records[invoiceCommitment].InvoiceEntry.Mask);
            byte[] diffCommitments = CryptoHelper.SubCommitments(paymentCommitment, _paymentSessions[sessionId].Records[invoiceCommitment].InvoiceEntry.Commitment);
            byte[] msg = _hashCalculation.CalculateHash(sessionId);
            var signature = CryptoHelper.GenerateBorromeanRingSignature(msg, new byte[][] { diffCommitments }, 0, diffBF);

            _paymentSessions[sessionId].Records[invoiceCommitment].PaymentSignature = signature;

            return new PaymentEntryDTO
            {
                SessionId = sessionId,
                Commitment = paymentCommitment.ToHexString(),
                RangeProof = rangeProof,
                Mask = paymentBlindingFactor.ToHexString()
            };
        }
    }
}
