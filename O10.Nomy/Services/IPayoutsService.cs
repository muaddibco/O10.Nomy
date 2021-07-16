using O10.Core.Architecture;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IPayoutsService
    {
        void Initialize(string walletId, string beneficiaryId, string senderId);
        Task CreatePayout(CancellationToken ct);

    }
}
