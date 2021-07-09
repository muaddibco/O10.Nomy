using O10.Core.Architecture;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IPaymentSessionsService
    {
        string CreatePaymentsSession();
        Task<PaymentSessionEntry?> PushInvoice(string sessionId, string currency, ulong amount);
        Task<PaymentSessionEntry?> PushPayment(string sessionId, string invoiceCommitment, string currency, ulong amount);
    }
}
