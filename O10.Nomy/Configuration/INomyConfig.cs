using O10.Core.Configuration;

namespace O10.Nomy.Configuration
{
    public interface INomyConfig : IConfigurationSection
    {
        string O10Uri { get; set; }
        string O10IdentityProvider { get; set; }
        int SessionTimeout { get; set; }
    }
}
