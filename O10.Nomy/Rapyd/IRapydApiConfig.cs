using O10.Core.Configuration;

namespace O10.Nomy.Rapyd
{
    public interface IRapydApiConfig : IConfigurationSection
    {
        string AccessKey { get; set; }
        string SecretKey { get; set; }
        string BaseUri { get; set; }
    }
}
