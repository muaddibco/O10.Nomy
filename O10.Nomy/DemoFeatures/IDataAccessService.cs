using O10.Nomy.DemoFeatures.Models;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    public partial interface IDataAccessService
    {
        Task<long> StoreDemoValidation(string action, string staticData, string dynamicData, CancellationToken cancellationToken);

        Task<DemoValidation> GetDemoValidation(long demoValiationId, CancellationToken cancellationToken);
    }
}
