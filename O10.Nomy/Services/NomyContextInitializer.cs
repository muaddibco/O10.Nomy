using O10.Client.Common.Entities;
using O10.Client.DataLayer.AttributesScheme;
using O10.Core;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Nomy.Configuration;
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

        public NomyContextInitializer(IO10ApiGateway o10ApiGateway, IConfigurationService configurationService, INomyContext nomyContext)
        {
            _o10ApiGateway = o10ApiGateway;
            _nomyContext = nomyContext;
            _nomyConfig = configurationService.Get<INomyConfig>();
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Normal;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            try
            {
                _nomyContext.O10IdentityProvider = await _o10ApiGateway.FindAccount(_nomyConfig.O10IdentityProvider);

                if (_nomyContext.O10IdentityProvider == null)
                {
                    _nomyContext.O10IdentityProvider = await _o10ApiGateway.RegisterIdp(_nomyConfig.O10IdentityProvider, "qqq");
                }

                await _o10ApiGateway.Start(_nomyContext.O10IdentityProvider.AccountId);

                var attrs = await _o10ApiGateway.GetAttributeDefinitions(_nomyContext.O10IdentityProvider.PublicSpendKey);

                if (attrs == null || attrs.Count == 0)
                {
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
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
