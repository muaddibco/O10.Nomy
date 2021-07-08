using O10.Core.Architecture;
using O10.Core.Configuration;

namespace O10.Nomy.Configuration
{
    [RegisterExtension(typeof(IConfigurationSection), Lifetime = LifetimeManagement.Singleton)]
    public class NomyConfig : ConfigurationSectionBase, INomyConfig
    {
        public const string SECTION_NAME = "AppSettings";

        public NomyConfig(IAppConfig appConfig) : base(appConfig, SECTION_NAME)
        {
        }

        public string O10Uri { get; set; }

        public string O10IdentityProvider { get; set; }
    }
}
