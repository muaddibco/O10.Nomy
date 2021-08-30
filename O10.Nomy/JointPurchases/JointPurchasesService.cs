using Flurl;
using O10.Client.Common.Communication;
using O10.Client.Web.DataContracts.ServiceProvider;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Core.Translators;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.JointPurchases.Models;
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
        private readonly ITranslatorsRepository _translatorsRepository;
        private readonly IDataAccessService _dataAccessService;
        private SignalrHubConnection? _hubConnection;
        private readonly ILogger _logger;
        private CancellationToken _cancellationToken;
        private O10AccountDTO _o10Account;

        public JointPurchasesService(IConfigurationService configurationService,
                                     IO10ApiGateway o10ApiGateway,
                                     ITranslatorsRepository translatorsRepository,
                                     IDataAccessService dataAccessService,
                                     ILoggerService loggerService)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _jointServiceConfiguration = configurationService.Get<IJointServiceConfiguration>();
            _o10ApiGateway = o10ApiGateway;
            _translatorsRepository = translatorsRepository;
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

        public async Task<JointGroupDTO> AddJointGroup(long o10RegistrationId, string name, string description)
        {
            var groupDto = await _o10ApiGateway.AddRelationGroup(_o10Account.AccountId, name);

            var group = await _dataAccessService.AddJointGroup(o10RegistrationId, groupDto.GroupId.Value, name, description, _cancellationToken);

            return _translatorsRepository.GetInstance<JointGroup, JointGroupDTO>().Translate(group);
        }

        public async Task<List<JointGroupDTO>> GetJointGroups(long o10RegistrationId)
        {
            var groups = await _dataAccessService.GetJointGroups(o10RegistrationId, _cancellationToken);

            return groups.Select(group => _translatorsRepository.GetInstance<JointGroup, JointGroupDTO>().Translate(group)).ToList();
        }

        public async Task<JointGroupMemberDTO> AddJointGroupMember(long groupId, string email, string? description)
        {
            var group = await _dataAccessService.GetJointGroup(groupId, _cancellationToken);

            var groupMemberDto = await _o10ApiGateway.AddRelation(_o10Account.AccountId, group.O10GroupId, email, description);

            var groupMember = await _dataAccessService.AddJointGroupMember(groupId, email, description, _cancellationToken);

            // here needs to go sending an email signed by the group administrator
            // how emails will be signed?
            // from field
            // to field

            return _translatorsRepository.GetInstance<JointGroupMember, JointGroupMemberDTO>().Translate(groupMember);
        }

        public async Task<List<JointGroupMemberDTO>> GetJointGroupMembers(long groupId)
        {
            var groupMembers = await _dataAccessService.GetJointGroupMembers(groupId, _cancellationToken);

            return groupMembers.Select(m => _translatorsRepository.GetInstance<JointGroupMember, JointGroupMemberDTO>().Translate(m)).ToList();
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
