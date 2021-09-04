using O10.Core.Architecture;
using O10.Nomy.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [ServiceContract]
    public interface IJointServiceUpdater
    {
        Task Initialize(CancellationToken cancellationToken);
        Task Start();

        O10AccountDTO Account { get; }
    }
}
