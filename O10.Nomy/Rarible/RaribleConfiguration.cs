using O10.Core.Architecture;
using O10.Core.Configuration;

namespace O10.Nomy.Rarible
{
    [RegisterExtension(typeof(IConfigurationSection), Lifetime = LifetimeManagement.Singleton)]
    public class RaribleConfiguration : ConfigurationSectionBase, IRaribleConfiguration
    {
        public const string SECTION_NAME = "Rarible";

        public RaribleConfiguration(IAppConfig appConfig) : base(appConfig, SECTION_NAME)
        {
        }

        public string BaseUri { get; set; }
    }
}
