using O10.Core;
using O10.Core.Architecture;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Scoped)]
    public class JointPurchasesServiceInitializer : InitializerBase
    {
        private readonly IJointServiceUpdater _jointServiceUpdater;

        public JointPurchasesServiceInitializer(IJointServiceUpdater jointServiceUpdater)
        {
            _jointServiceUpdater = jointServiceUpdater;
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Lowest;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            await _jointServiceUpdater.Initialize(cancellationToken);
        }
    }
}
