using O10.Core.Architecture;
using O10.Nomy.DTOs;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface INomyContext
    {
        O10AccountDTO? O10IdentityProvider { get; set; }

        string GetBaseUrl();
    }
}
