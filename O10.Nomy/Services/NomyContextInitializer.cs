using O10.Client.Common.Entities;
using O10.Client.DataLayer.AttributesScheme;
using O10.Core;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Nomy.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Singleton)]
    public class NomyContextInitializer : InitializerBase
    {
        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly INomyContext _nomyContext;
        private readonly INomyConfig _nomyConfig;
        private readonly ILogger _logger;

        public NomyContextInitializer(IO10ApiGateway o10ApiGateway, IConfigurationService configurationService, ILoggerService loggerService, INomyContext nomyContext)
        {
            _o10ApiGateway = o10ApiGateway;
            _nomyContext = nomyContext;
            _nomyConfig = configurationService.Get<INomyConfig>();
            _logger = loggerService.GetLogger(nameof(NomyContextInitializer));
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Normal;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            try
            {
                _logger.Debug($"Looking for O10 account of {_nomyConfig.O10IdentityProvider}");
                _nomyContext.O10IdentityProvider = await _o10ApiGateway.FindAccount(_nomyConfig.O10IdentityProvider);

                if (_nomyContext.O10IdentityProvider == null)
                {
                    _logger.Debug($"No O10 account found for {_nomyConfig.O10IdentityProvider}, registering a new account");
                    _nomyContext.O10IdentityProvider = await _o10ApiGateway.RegisterIdp(_nomyConfig.O10IdentityProvider, "qqq");
                    _logger.Debug($"O10 account of {_nomyConfig.O10IdentityProvider} created with accountId {_nomyContext.O10IdentityProvider.AccountId}");
                }

                _logger.Debug($"Starting O10 account of {_nomyConfig.O10IdentityProvider}");
                await _o10ApiGateway.Start(_nomyContext.O10IdentityProvider.AccountId);

                _logger.Debug($"Checking Scheme Definition of {_nomyConfig.O10IdentityProvider}");
                var attrs = await _o10ApiGateway.GetAttributeDefinitions(_nomyContext.O10IdentityProvider.PublicSpendKey);

                if (attrs == null || attrs.Count == 0)
                {
                    _logger.Debug($"No Scheme defined by {_nomyConfig.O10IdentityProvider}, defining a new one");
                    List<AttributeDefinition> attributeDefitions = new()
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "Email",
                            Alias = "Email",
                            Description = "Email",
                            IsRoot = true,
                            IsActive = true,
                            SchemeName = AttributesSchemes.ATTR_SCHEME_NAME_EMAIL
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "FirstName",
                            Alias = "First Name",
                            Description = "First Name",
                            IsRoot = false,
                            IsActive = true,
                            SchemeName = AttributesSchemes.ATTR_SCHEME_NAME_FIRSTNAME
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "LastName",
                            Alias = "Last Name",
                            Description = "Last Name",
                            IsRoot = false,
                            IsActive = true,
                            SchemeName = AttributesSchemes.ATTR_SCHEME_NAME_LASTNAME
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "RapydWalletId",
                            Alias = "Rapyd Wallet ID",
                            Description = "Rapyd Wallet ID",
                            IsRoot = false,
                            IsActive = true,
                            SchemeName = AttributesSchemes.ATTR_SCHEME_NAME_DRIVINGLICENSE
                        }
                    };

                    var resp = await _o10ApiGateway.SetAttributeDefinitions(_nomyContext.O10IdentityProvider.PublicSpendKey, attributeDefitions);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to intialize {_nomyConfig.O10IdentityProvider}", ex);
                throw;
            }
        }
    }
}
