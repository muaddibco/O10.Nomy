using Microsoft.AspNetCore.Http;
using O10.Core.Architecture;
using O10.Nomy.DTOs;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(INomyContext), Lifetime = LifetimeManagement.Singleton)]
    public class NomyContext : INomyContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NomyContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public O10AccountDTO? O10IdentityProvider { get; set; }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            var host = request?.Host.ToUriComponent();

            var pathBase = request?.PathBase.ToUriComponent();

            return $"{request?.Scheme}://{host}{pathBase}";
        }
    }
}
