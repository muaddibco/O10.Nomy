using O10.Core.Architecture;
using O10.Nomy.DTOs;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(INomyContext), Lifetime = LifetimeManagement.Singleton)]
    public class NomyContext : INomyContext
    {
        public O10AccountDTO? O10IdentityProvider { get; set; }
    }
}
