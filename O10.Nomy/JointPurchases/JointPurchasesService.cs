using Flurl;
using O10.Client.Common.Communication;
using O10.Client.Web.DataContracts.ServiceProvider;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.Models;
using O10.Nomy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [RegisterDefaultImplementation(typeof(IJointPurchasesService), Lifetime = LifetimeManagement.Singleton)]
    public class JointPurchasesService : IJointPurchasesService
    {
        private readonly INomyConfig _nomyConfig;
        private readonly IJointServiceConfiguration _jointServiceConfiguration;
        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly IDataAccessService _dataAccessService;
        private SignalrHubConnection? _hubConnection;
        private readonly ILogger _logger;
        private CancellationToken _cancellationToken;
        private O10AccountDTO _o10Account;

        public JointPurchasesService(IConfigurationService configurationService,
                                     IO10ApiGateway o10ApiGateway,
                                     IDataAccessService dataAccessService,
                                     ILoggerService loggerService)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _jointServiceConfiguration = configurationService.Get<IJointServiceConfiguration>();
            _o10ApiGateway = o10ApiGateway;
            _dataAccessService = dataAccessService;
            _logger = loggerService.GetLogger(nameof(JointPurchasesService));
            _logger.SetContext(nameof(JointPurchasesService));
        }

        /// <summary>
        /// 1. Create Joint Service Account if not exist yet
        /// 2. Start it
        /// 3. 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Initialize(CancellationToken ct)
        {
            var account = await _o10ApiGateway.FindAccount(_jointServiceConfiguration.JointServiceName);
            if(account == null)
            {
                account = await _o10ApiGateway.RegisterServiceProvider(_jointServiceConfiguration.JointServiceName, "qqq");
            }

            _o10Account = await _o10ApiGateway.Authenticate(account.AccountId, "qqq");

            var user = await GetJointServiceRecord();
            if (user == null)
            {
                user = await _dataAccessService.CreateServiceProvider(_o10Account.AccountId, _jointServiceConfiguration.JointServiceName, _cancellationToken);
            }

            _cancellationToken = ct;
        }

        public Task<NomyServiceProvider> GetJointServiceRecord()
        {
            return _dataAccessService.FindServiceProvider(_jointServiceConfiguration.JointServiceName, _cancellationToken);
        }

        private async Task BuildHubConnection()
        {
            string signalrHubUri = $"{_nomyConfig.O10Uri.AppendPathSegment("identitiesHub")}";

            if (_hubConnection != null)
            {
                await _hubConnection.DestroyHubConnection();
            }

            _hubConnection = new SignalrHubConnection(new Uri(signalrHubUri), nameof(JointPurchasesService), _logger, _cancellationToken);
            await _hubConnection.BuildHubConnection();

            _logger.Info($"SignalRPacketsProvider created instance of hubConnection to URI {signalrHubUri}");

            _hubConnection.On<ServiceProviderRegistrationExDto>("PushRegistration", p =>
            {

            });

            _hubConnection.On<ServiceProviderRegistrationExDto>("PushAuthorizationSucceeded", p =>
            {

            });
        }
    }
}
