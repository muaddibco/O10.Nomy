using O10.Core.Architecture;
using O10.Core.Configuration;

namespace O10.Nomy.JointPurchases
{
    [RegisterExtension(typeof(IConfigurationSection), Lifetime = LifetimeManagement.Singleton)]
    public class JointServiceConfiguration : ConfigurationSectionBase, IJointServiceConfiguration
    {
        public const string NAME = "JointSerivce";

        public JointServiceConfiguration(IAppConfig appConfig) : base(appConfig, NAME)
        {
        }

        public string JointServiceName { get; set; }
    }
}
