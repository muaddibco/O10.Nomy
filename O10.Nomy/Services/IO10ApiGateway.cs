﻿using O10.Client.Common.Entities;
using O10.Client.Web.DataContracts;
using O10.Client.Web.DataContracts.ServiceProvider;
using O10.Client.Web.DataContracts.User;
using O10.Core.Architecture;
using O10.Nomy.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IO10ApiGateway
    {
        Task<O10AccountDTO?> FindAccount(string alias);
        Task<O10AccountDTO?> FindAccountByKeys(DisclosedSecretsDto disclosedSecrets);
        Task<O10AccountDTO?> GetAccount(long accountId);

        Task<O10AccountDTO?> ResetAccount(long accountId, string password);

        Task<O10AccountDTO?> RegisterIdp(string alias, string password);
        Task<O10AccountDTO?> RegisterServiceProvider(string alias, string password);
        Task<O10AccountDTO?> RegisterUser(string email, string password);
        Task<O10AccountDTO?> DuplicateAccount(long sourceAccountId, long targetAccountId);
        Task<O10AccountDTO?> OverrideAccount(long accountId, DisclosedSecretsDto disclosedSecrets);
        
        Task<O10AccountDTO?> Authenticate(long accountId, string password);
        Task SyncAccount(long accountId);
        Task StopAccount(long accountId);
        Task<bool> IsAuthenticated(long accountId, CancellationToken ct);

        Task<IEnumerable<AttributeValue>> RequestIdentity(long accountId, string password, string email, string firstName, string lastName, string walletId);

        Task<UserAccountDetailsDto> GetUserAccountDetails(long accountId);

        Task<List<UserAttributeSchemeDto>?> GetUserAttributes(long accountId);

        Task<List<AttributeDefinition>> GetAttributeDefinitions(string issuer);

        Task<AttributeDefinitionsResponse> SetAttributeDefinitions(string issuer, List<AttributeDefinition> attributeDefinitions);

        Task<QrCodeDto> GetSessionInfo(long accountId);

        Task<UserActionInfoDto> GetUserActionInfo(string encodedAction);

        Task<UserActionCodeDto> GetDiscloseSecretsCode(long accountId, string password);

        Task<ActionDetailsDto> GetActionDetails(long accountId, string encodedAction, long userAttributeId);

        Task SendUniversalProofs(long accountId, UniversalProofsSendingRequest universalProofs);

        Task<RelationGroupDto> AddRelationGroup(long accountId, string groupName);
        Task<RelationDto> AddRelation(long accountId, long groupId, string rawRootAttributeValue, string description);

        Task SendCompromizationClaim(long accountId, UnauthorizedUseDto unauthorizedUse);
    }
}
