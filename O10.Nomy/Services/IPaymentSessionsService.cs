using O10.Core.Architecture;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IPaymentSessionsService
    {
        string CreatePaymentsSession();
        Task<PaymentSessionEntry?> PushInvoice(long userId, string sessionId, string currency, ulong amount, CancellationToken ct);
        Task<PaymentSessionEntry?> PushPayment(long userId, string sessionId, string invoiceCommitment, string currency, ulong amount, CancellationToken ct);
    }
}
