using O10.Core;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Nomy.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using O10.Core.Logging;

namespace O10.Nomy.Services
{
    public class WaitForPortalInitializer : InitializerBase
    {
        private readonly INomyConfig _nomyConfig;
        private readonly ILogger _logger;

        public WaitForPortalInitializer(IConfigurationService configurationService, ILoggerService loggerService)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _logger = loggerService.GetLogger(nameof(WaitForPortalInitializer));
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.AboveNormal;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            while(!await GetIsPortalHealth(cancellationToken))
            {
                _logger.Info("Portal is not ready, recheck in 10s...");
                await Task.Delay(10000, cancellationToken);
            }

            _logger.Info("Portal is ready");
        }

        private async Task<bool> GetIsPortalHealth(CancellationToken ct)
        {
            try
            {
                var resp = await _nomyConfig.O10Uri.AppendPathSegment("Health").GetStringAsync(ct);
                return "Healthy".Equals(resp, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
