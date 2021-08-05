using O10.Core.Configuration;

namespace O10.Nomy.JointPurchases
{
    public interface IJointServiceConfiguration : IConfigurationSection
    {
        public string JointServiceName { get; set; }
    }
}
