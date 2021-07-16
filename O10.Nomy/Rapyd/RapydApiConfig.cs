using O10.Core.Architecture;
using O10.Core.Configuration;

namespace O10.Nomy.Rapyd
{
    [RegisterExtension(typeof(IConfigurationSection), Lifetime = LifetimeManagement.Singleton)]
    public class RapydApiConfig : ConfigurationSectionBase, IRapydApiConfig
    {
        public const string NAME = "RapydApi";

        public RapydApiConfig(IAppConfig appConfig) : base(appConfig, NAME)
        {
        }

        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BaseUri { get; set; }
    }
}
