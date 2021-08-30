using O10.Core.Configuration;

namespace O10.Nomy.Rarible
{
    public interface IRaribleConfiguration : IConfigurationSection
    {
        string BaseUri { get; set; }
    }
}
