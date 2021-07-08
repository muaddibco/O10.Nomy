using Microsoft.AspNetCore.SignalR;
using O10.Core.Architecture;
using O10.Core.Cryptography;
using O10.Core.ExtensionMethods;
using O10.Core.HashCalculations;
using O10.Crypto.ConfidentialAssets;
using O10.Nomy.DTOs;
using O10.Nomy.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(IPaymentSessionsService), Lifetime = LifetimeManagement.Singleton)]
    public class PaymentSessionsService : IPaymentSessionsService
    {
        private readonly IHashCalculation _hashCalculation;
        private readonly Dictionary<string, PaymentSession> _paymentSessions = new();
        private readonly IHubContext<PaymentSessionHub> _hubContext;

        public PaymentSessionsService(IHashCalculationsRepository hashCalculationsRepository, IHubContext<PaymentSessionHub> hubContext)
        {
            _hashCalculation = hashCalculationsRepository.Create(HashType.Keccak256);
            _hubContext = hubContext;
        }

        public string CreatePaymentsSession()
        {
            string sessionId = Guid.NewGuid().ToString();

            _paymentSessions.Add(sessionId, new PaymentSession { SessionId = sessionId });

            return sessionId;
        }

        public async Task PushInvoice(string sessionId, string currency, ulong amount)
        {
            var invoiceEntry = GenerateInvoiceEntry(sessionId, currency, amount);

            var record = _paymentSessions[sessionId].Records[invoiceEntry.Commitment];

            await _hubContext.Clients.Group($"{sessionId}_Payer").SendAsync("Invoice", invoiceEntry);

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

        }

        public async Task PushPayment(string sessionId, string invoiceCommitment, string currency, ulong amount)
        {
            lock(_paymentSessions)
            {
                if (!_paymentSessions.ContainsKey(sessionId))
                {
                    return;
                }

                _paymentSessions[sessionId].Records[invoiceCommitment].TimeoutCancellation.Cancel();
            }

            var paymentEntry = GeneratePaymentEntry(sessionId, invoiceCommitment, currency, amount);

            byte[] diffCommitments = CryptoHelper.SubCommitments(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentEntry.Commitment, _paymentSessions[sessionId].Records[invoiceCommitment].InvoiceEntry.Commitment);
            byte[] msg = _hashCalculation.CalculateHash(sessionId);
            bool resDiff = CryptoHelper.VerifyRingSignature(_paymentSessions[sessionId].Records[invoiceCommitment].PaymentSignature, msg, new byte[][] { diffCommitments });

            if(resDiff)
            {
                await _hubContext.Clients.Group($"{sessionId}_Payee").SendAsync("Payment", paymentEntry);
            }
            else
            {
                await _hubContext.Clients.Group($"{sessionId}_Payee").SendAsync("Error", paymentEntry);
            }
        }

        private PaymentEntryDTO GenerateInvoiceEntry(string sessionId, string currency, ulong amount)
        {
            byte[] currencyCommitment = CryptoHelper.GetNonblindedAssetCommitment(_hashCalculation.CalculateHash(currency));
            RangeProof rangeProof = CryptoHelper.ProveRange(out byte[] invoiceCommitment, out byte[] invoiceBlindingFactor, amount, currencyCommitment);

            var record = new PaymentSessionRecord
            {
                InvoiceEntry = new PaymentSessionEntry { Commitment = invoiceCommitment, Mask = invoiceBlindingFactor, RangeProof = rangeProof },
            };

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
