using O10.Core;
using O10.Core.Architecture;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Singleton)]
    public class JointPurchasesServiceInitializer : InitializerBase
    {
        private readonly IJointPurchasesService _jointPurchasesService;

        public JointPurchasesServiceInitializer(IJointPurchasesService jointPurchasesService)
        {
            _jointPurchasesService = jointPurchasesService;
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Lowest;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            await _jointPurchasesService.Initialize(cancellationToken);
        }
    }
}
