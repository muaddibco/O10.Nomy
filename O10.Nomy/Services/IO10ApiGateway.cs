using O10.Client.Common.Entities;
using O10.Client.Web.DataContracts;
using O10.Client.Web.DataContracts.ServiceProvider;
using O10.Client.Web.DataContracts.User;
using O10.Core.Architecture;
using O10.Nomy.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IO10ApiGateway
    {
        Task<O10AccountDTO?> FindAccount(string alias);
        Task<O10AccountDTO?> GetAccount(long accountId);

        Task<O10AccountDTO?> RegisterIdp(string alias, string password);
        Task<O10AccountDTO?> RegisterServiceProvider(string alias, string password);
        Task<O10AccountDTO?> RegisterUser(string email, string password);

        Task<O10AccountDTO?> Start(long accountId);
        
        Task SetBindingKey(long accountId, string password);
        Task<O10AccountDTO?> Authenticate(long accountId, string password);

        Task<IEnumerable<AttributeValue>> RequestIdentity(long accountId, string password, string email, string firstName, string lastName, string walletId);

        Task<List<UserAttributeSchemeDto>?> GetUserAttributes(long accountId);

        Task<List<AttributeDefinition>> GetAttributeDefinitions(string issuer);

        Task<AttributeDefinitionsResponse> SetAttributeDefinitions(string issuer, List<AttributeDefinition> attributeDefinitions);

        Task<QrCodeDto> GetSessionInfo(long accountId);

        Task<UserActionInfoDto> GetUserActionInfo(string encodedAction);

        Task<ActionDetailsDto> GetActionDetails(long accountId, string encodedAction, long userAttributeId);

        Task SendUniversalProofs(long accountId, UniversalProofsSendingRequest universalProofs);

        Task<RelationGroupDto> AddRelationGroup(long accountId, string groupName);
        Task<RelationDto> AddRelation(long accountId, long groupId, string rawRootAttributeValue, string description);
    }
}
