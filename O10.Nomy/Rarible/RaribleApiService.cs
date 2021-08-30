using O10.Core.Architecture;
using O10.Nomy.Rarible.Models;
using System.Threading.Tasks;
using O10.Core.Configuration;
using Flurl;
using O10.Core.Logging;
using Flurl.Http;

namespace O10.Nomy.Rarible
{
    [RegisterDefaultImplementation(typeof(IRaribleApiService), Lifetime = LifetimeManagement.Singleton)]
    public class RaribleApiService : IRaribleApiService
    {
        private readonly IRaribleConfiguration _raribleConfiguration;
        private readonly ILogger _logger;

        public RaribleApiService(IConfigurationService configurationService, ILoggerService loggerService)
        {
            _raribleConfiguration = configurationService.Get<IRaribleConfiguration>();
            _logger = loggerService.GetLogger(nameof(RaribleApiService));
        }

        public async Task<AllItemsDto> GetAllItems(string continuationToken = null)
        {
            var req = _raribleConfiguration.BaseUri
                .AppendPathSegments("nft", "items")
                .SetQueryParam("continuation", continuationToken);

            _logger.Debug(() => $"Sending Rarible request {req}");

            var resp = await req.GetJsonAsync<AllItemsDto>();

            return resp;
        }
    }
}
