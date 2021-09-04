using O10.Core.Architecture;
using O10.Nomy.Rapyd;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(IPayoutsService), Lifetime = LifetimeManagement.Scoped)]
    public class PayoutsService : IPayoutsService
    {
        private readonly IDataAccessService _dataAccessService;
        private readonly IRapydService _rapydService;
        private string _walletId, _beneficiaryId, _senderId;

        public PayoutsService(IDataAccessService dataAccessService, IRapydService rapydService)
        {
            _dataAccessService = dataAccessService;
            _rapydService = rapydService;
        }

        public void Initialize(string walletId, string beneficiaryId, string senderId)
        {
            _walletId = walletId;
            _beneficiaryId = beneficiaryId;
            _senderId = senderId;
        }

        public async Task CreatePayout(CancellationToken ct)
        {
            var payouts = _dataAccessService.GetPayouts();
            DateTime from = payouts?.OrderByDescending(p => p.To).FirstOrDefault()?.To ?? DateTime.MinValue;

            var invoices = await _dataAccessService.GetInvoiceRecords(true, ct);
            var payments = await _dataAccessService.GetPaymentRecords(true, ct);

            var secretInvoices = _dataAccessService.GetSecretInvoices(invoices.Select(i => i.InvoiceRecordId));
            var secretPayments = _dataAccessService.GetSecretPayments(payments.Select(i => i.PaymentRecordId));

            var invoicesByUser = secretInvoices.GroupBy(i => i.User);
            var paymentsByUser = secretPayments.GroupBy(i => i.User);

            foreach (var g in paymentsByUser)
            {
                var amount = g.Sum(s => (long)s.Amount);

                await _rapydService.TransferFunds(g.Key.WalletId, _walletId, "USD", (ulong)amount);
            }

            foreach (var g in invoicesByUser)
            {
                var amount = g.Sum(s => (long)s.Amount);

                await _rapydService.PayoutFunds(_walletId, g.Key.BeneficiaryId, "USD", (ulong)amount);
            }
        }
    }
}
