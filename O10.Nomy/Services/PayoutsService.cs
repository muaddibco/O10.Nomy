using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    public class PayoutsService : IPayoutsService
    {
        private readonly IDataAccessService _dataAccessService;

        public PayoutsService(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public async Task CollectPayments(CancellationToken ct)
        {
            var payouts = _dataAccessService.GetPayouts();
            DateTime from = payouts?.OrderByDescending(p => p.To).FirstOrDefault()?.To ?? DateTime.MinValue;

            var invoices = await _dataAccessService.GetInvoiceRecords(true, ct);
            var payments = await _dataAccessService.GetPaymentRecords(true, ct);

            var secretInvoices = _dataAccessService.GetSecretInvoices(invoices.Select(i => i.InvoiceRecordId));
            var secretPayments = _dataAccessService.GetSecretPayments(payments.Select(i => i.PaymentRecordId));


        }
    }
}
