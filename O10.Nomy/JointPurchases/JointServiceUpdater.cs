using Flurl;
using Microsoft.Extensions.DependencyInjection;
using O10.Client.Common.Communication;
using O10.Client.Web.DataContracts.ServiceProvider;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [RegisterDefaultImplementation(typeof(IJointServiceUpdater), Lifetime = LifetimeManagement.Singleton)]
    public class JointServiceUpdater : IJointServiceUpdater
    {
        private SignalrHubConnection? _hubConnection;
        private readonly ILogger _logger;
        private readonly INomyConfig _nomyConfig;
        private readonly IDataAccessService _dataAccessService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IJointServiceConfiguration _jointServiceConfiguration;
        private readonly IO10ApiGateway _o10ApiGateway;
        private CancellationToken _cancellationToken;

        public JointServiceUpdater(IConfigurationService configurationService,
                                   IO10ApiGateway o10ApiGateway,
                                   ILoggerService loggerService,
                                   IServiceProvider serviceProvider)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _jointServiceConfiguration = configurationService.Get<IJointServiceConfiguration>();
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _dataAccessService = _serviceProvider.GetService<IDataAccessService>();
            _logger = loggerService.GetLogger(nameof(JointServiceUpdater));
            _o10ApiGateway = o10ApiGateway;
        }

        public O10AccountDTO Account { get; private set; }

        public async Task Initialize(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            var account = await _o10ApiGateway.FindAccount(_jointServiceConfiguration.JointServiceName);
            if (account == null)
            {
                account = await _o10ApiGateway.RegisterServiceProvider(_jointServiceConfiguration.JointServiceName, "qqq");
            }

            Account = await _o10ApiGateway.Authenticate(account.AccountId, "qqq");

            var user = await _dataAccessService.FindServiceProvider(_jointServiceConfiguration.JointServiceName, cancellationToken);
            if (user == null)
            {
                user = await _dataAccessService.CreateServiceProvider(Account.AccountId, _jointServiceConfiguration.JointServiceName, cancellationToken);
            }

            await BuildHubConnection();
        }

        public async Task Start()
        {
            await _hubConnection.StartHubConnection();
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

            _hubConnection.On<ServiceProviderRegistrationExDto>("PushRegistration", async p =>
            {
                await _dataAccessService.GetOrAddJointServiceRegistration(p.RegistrationId, p.Commitment, _cancellationToken);
            });

            _hubConnection.On<ServiceProviderRegistrationExDto>("PushAuthorizationSucceeded", async p =>
            {
                await _dataAccessService.GetOrAddJointServiceRegistration(p.RegistrationId, p.Commitment, _cancellationToken);
            });
        }
    }
}
