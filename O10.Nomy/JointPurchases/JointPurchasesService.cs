using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Core.Translators;
using O10.Nomy.Configuration;
using O10.Nomy.JointPurchases.Models;
using O10.Nomy.Models;
using O10.Nomy.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [RegisterDefaultImplementation(typeof(IJointPurchasesService), Lifetime = LifetimeManagement.Scoped)]
    public class JointPurchasesService : IJointPurchasesService
    {
        private readonly INomyConfig _nomyConfig;
        private readonly IJointServiceConfiguration _jointServiceConfiguration;
        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly IJointServiceUpdater _jointServiceUpdater;
        private readonly ITranslatorsRepository _translatorsRepository;
        private readonly IDataAccessService _dataAccessService;
        private readonly ILogger _logger;

        public JointPurchasesService(IConfigurationService configurationService,
                                     IO10ApiGateway o10ApiGateway,
                                     IJointServiceUpdater jointServiceUpdater,
                                     ITranslatorsRepository translatorsRepository,
                                     IDataAccessService dataAccessService,
                                     ILoggerService loggerService)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _jointServiceConfiguration = configurationService.Get<IJointServiceConfiguration>();
            _o10ApiGateway = o10ApiGateway;
            _jointServiceUpdater = jointServiceUpdater;
            _translatorsRepository = translatorsRepository;
            _dataAccessService = dataAccessService;
            _logger = loggerService.GetLogger(nameof(JointPurchasesService));
            _logger.SetContext(nameof(JointPurchasesService));
        }

        public Task<NomyServiceProvider> GetJointServiceRecord(CancellationToken cancellationToken)
        {
            return _dataAccessService.FindServiceProvider(_jointServiceConfiguration.JointServiceName, cancellationToken);
        }

        public async Task<JointGroupDTO> AddJointGroup(long o10RegistrationId, string name, string description, CancellationToken ct)
        {
            var groupDto = await _o10ApiGateway.AddRelationGroup(_jointServiceUpdater.Account.AccountId, name);
            

            var group = await _dataAccessService.AddJointGroup(o10RegistrationId, groupDto.GroupId.Value, name, description, ct);

            return _translatorsRepository.GetInstance<JointGroup, JointGroupDTO>().Translate(group);
        }

        public async Task<List<JointGroupDTO>> GetJointGroups(long o10RegistrationId, CancellationToken ct)
        {
            var groups = await _dataAccessService.GetJointGroups(o10RegistrationId, ct);

            return groups.Select(group => _translatorsRepository.GetInstance<JointGroup, JointGroupDTO>().Translate(group)).ToList();
        }

        public async Task<JointGroupMemberDTO> AddJointGroupMember(long groupId, string email, string? description, CancellationToken ct)
        {
            var group = await _dataAccessService.GetJointGroup(groupId, ct);

            var groupMemberDto = await _o10ApiGateway.AddRelation(_jointServiceUpdater.Account.AccountId, group.O10GroupId, email, description);

            var groupMember = await _dataAccessService.AddJointGroupMember(groupId, email, description, ct);

            // here needs to go sending an email signed by the group administrator
            // how emails will be signed?
            // from field
            // to field
            //
            // "From" field is a email address of the group admin

            MailDescriptor mailDescriptor = new MailDescriptor
            {

            };

            return _translatorsRepository.GetInstance<JointGroupMember, JointGroupMemberDTO>().Translate(groupMember);
        }

        public async Task<List<JointGroupMemberDTO>> GetJointGroupMembers(long groupId, CancellationToken ct)
        {
            var groupMembers = await _dataAccessService.GetJointGroupMembers(groupId, ct);

            return groupMembers.Select(m => _translatorsRepository.GetInstance<JointGroupMember, JointGroupMemberDTO>().Translate(m)).ToList();
        }
    }
}
