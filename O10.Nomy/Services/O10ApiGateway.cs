using O10.Client.Common.Entities;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Nomy.Configuration;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using O10.Nomy.DTOs;
using System.Collections.Generic;
using O10.Core.Logging;
using Newtonsoft.Json;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(IO10ApiGateway), Lifetime = LifetimeManagement.Singleton)]
    public class O10ApiGateway : IO10ApiGateway
    {
        private readonly INomyConfig _nomyConfig;
        private readonly INomyContext _nomyContext;
        private readonly ILogger _logger;

        public O10ApiGateway(IConfigurationService configurationService, INomyContext nomyContext, ILoggerService loggerService)
        {
            _nomyConfig = configurationService.Get<INomyConfig>();
            _nomyContext = nomyContext;
            _logger = loggerService.GetLogger(nameof(O10ApiGateway));
        }

        public async Task<O10AccountDTO?> FindAccount(string alias)
        {
            var req = _nomyConfig.O10Uri
                .AppendPathSegments("accounts", "find")
                .SetQueryParam("accountAlias", alias);

            _logger.Debug(() => $"Sending O10 request {req}");
            
            var account = await req.GetJsonAsync<O10AccountDTO?>();

            return account;
        }

        public async Task<O10AccountDTO?> GetAccount(long accountId)
        {
            var req = _nomyConfig.O10Uri.AppendPathSegments("accounts", accountId.ToString());

            _logger.Debug(() => $"Sending O10 request {req}");

            var account = await req.GetJsonAsync<O10AccountDTO?>();

            return account;
        }

        public async Task<List<UserAttributeSchemeDto>?> GetUserAttributes(long accountId)
        {
            var attributeSchemes = await _nomyConfig.O10Uri
                .AppendPathSegments("user", accountId.ToString(), "attributes")
                .GetJsonAsync<List<UserAttributeSchemeDto>?>();

            return attributeSchemes;
        }

        public async Task<O10AccountDTO?> RegisterIdp(string alias, string password)
        {
            var req = _nomyConfig.O10Uri.AppendPathSegments("accounts", "register");
            var body = new
            {
                accountType = 1,
                accountInfo = alias,
                password
            };

            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(body, Formatting.Indented)}");

            var account = await req
                .PostJsonAsync(body)
                .ReceiveJson<O10AccountDTO>();

            return account;
        }

        public async Task<O10AccountDTO?> RegisterUser(string email, string password)
        {
            var req = _nomyConfig.O10Uri
                .AppendPathSegments("accounts", "register");

            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(new { accountType = 3, accountInfo = email, password }, Formatting.Indented)}");

            var account = await req
                .PostJsonAsync(new
                {
                    accountType = 3,
                    accountInfo = email,
                    password
                })
                .ReceiveJson<O10AccountDTO>();

            return account;
        }

        public async Task<IEnumerable<AttributeValue>> RequestIdentity(long accountId, string password, string email, string firstName, string lastName, string walletId)
        {
            var req = _nomyConfig.O10Uri.AppendPathSegments("user", accountId, "Attributes");
            var body = new
            {
                issuer = _nomyContext.O10IdentityProvider.PublicSpendKey,
                attributeValues = new
                {
                    email,
                    firstName,
                    lastName,
                    rapydWalletId = walletId
                }
            };

            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(body, Formatting.Indented)}");

            return await req.PostJsonAsync(body).ReceiveJson<IEnumerable<AttributeValue>>();
        }

        public async Task SetBindingKey(long accountId, string password)
        {
            var req = _nomyConfig.O10Uri
                .AppendPathSegments("Accounts", accountId.ToString(), "BindingKey");
            
            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(new { password }, Formatting.Indented)}");

            await req.PostJsonAsync(new { password });
        }

        public async Task<O10AccountDTO?> Authenticate(long accountId, string password)
        {
            var req = _nomyConfig.O10Uri
                .AppendPathSegments("Accounts", "Authenticate");

            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(new { accountId, password }, Formatting.Indented)}");

            return await req
                .PostJsonAsync(new { accountId, password })
                .ReceiveJson<O10AccountDTO?>();
        }

        public async Task<O10AccountDTO?> Start(long accountId)
        {
            var req = _nomyConfig.O10Uri
                .AppendPathSegments("Accounts", accountId.ToString(), "Start");

            _logger.Debug(() => $"Sending O10 request {req}");

            var account = await req
                .PostAsync()
                .ReceiveJson<O10AccountDTO>();

            return account;
        }

        public async Task<List<AttributeDefinition>> GetAttributeDefinitions(string issuer)
        {
            var attributeDefinitions = await _nomyConfig.O10Uri
                .AppendPathSegments("SchemeResolution", "AttributeDefinitions")
                .SetQueryParam("issuer", issuer)
                .GetJsonAsync<List<AttributeDefinition>>();

            return attributeDefinitions;
        }

        public async Task<AttributeDefinitionsResponse> SetAttributeDefinitions(string issuer, List<AttributeDefinition> attributeDefinitions)
        {
            var req = _nomyConfig.O10Uri.AppendPathSegments("SchemeResolution", "AttributeDefinitions").SetQueryParam("issuer", issuer);

            _logger.Debug(() => $"Sending O10 request {req}\r\n{JsonConvert.SerializeObject(attributeDefinitions, Formatting.Indented)}");

            var resp = await req
                .PutJsonAsync(attributeDefinitions)
                .ReceiveJson<AttributeDefinitionsResponse>();

            return resp;
        }
    }
}
