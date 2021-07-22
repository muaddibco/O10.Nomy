using O10.Client.Common.Entities;
using O10.Client.DataLayer.AttributesScheme;
using O10.Core;
using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.Rapyd;
using O10.Nomy.Rapyd.DTOs;
using O10.Nomy.Rapyd.DTOs.Disburse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Singleton)]
    public class NomyContextInitializer : InitializerBase
    {
        public readonly UserDTO _demoUser = new()
        {
            Email = "demo@nomy.com",
            FirstName = "John",
            LastName = "Doe"
        };

        public readonly List<ExpertiseAreaDTO> _expertiseAreas = new()
        {
            new ExpertiseAreaDTO
            {
                Name = "Psychology",
                ExpertProfiles = new List<ExpertProfileDTO>
                {
                    new ExpertProfileDTO
                    {
                        Email = "floria.pham@nomy.com",
                        FirstName = "Floria",
                        LastName = "Pham",
                        ExpertiseSubAreas = new List<string> { "Child psychology", "Clinical psychology" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "donita.almanda@nomy.com",
                        FirstName = "Donita",
                        LastName = "Almanda",
                        ExpertiseSubAreas = new List<string> { "Forensic psychology", "Evolutionary psychology" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "alana.hartshorn@nomy.com",
                        FirstName = "Alana",
                        LastName = "Hartshorn",
                        ExpertiseSubAreas = new List<string> { "Occupational psychology", "Social psychology" }
                    }
                }
            },
            new ExpertiseAreaDTO
            {
                Name = "Psychiatry",
                ExpertProfiles = new List<ExpertProfileDTO>
                {
                    new ExpertProfileDTO
                    {
                        Email = "joseph.yzaguirre@nomy.com",
                        FirstName = "Joseph",
                        LastName = "Yzaguirre",
                        ExpertiseSubAreas = new List<string>{"Old age psychiatry", "Psychiatry of intellectual disability" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "monty.escareno@nomy.com",
                        FirstName = "Monty",
                        LastName = "Escareno",
                        ExpertiseSubAreas = new List<string> { "Child and adolescent psychiatry", "General psychiatry" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "nadine.ralston@nomy.com",
                        FirstName = "Nadine",
                        LastName = "Ralston",
                        ExpertiseSubAreas = new List<string> { "Forensic psychiatry" }
                    }
                }
            },
            new ExpertiseAreaDTO
            {
                Name = "Lawyer",
                ExpertProfiles = new List<ExpertProfileDTO>
                {
                    new ExpertProfileDTO
                    {
                        Email = "holley.wilsey@nomy.com",
                        FirstName = "Holley",
                        LastName = "Wilsey",
                        ExpertiseSubAreas = new List<string> { "Personal Injury Lawyer", "Criminal Lawyer" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "dudley.luiz@nomy.com",
                        FirstName = "Dudley",
                        LastName = "Luiz",
                        ExpertiseSubAreas = new List<string> { "Bankruptcy Lawyer", "Employment Lawyer", "Corporate Lawyer" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "phylicia.atilano@nomy.com",
                        FirstName = "Phylicia",
                        LastName = "Atilano",
                        ExpertiseSubAreas = new List<string> { "Immigration Lawyer", "Criminal Lawyer" }
                    }
                }
            },
            new ExpertiseAreaDTO
            {
                Name = "Accounting",
                ExpertProfiles = new List<ExpertProfileDTO>
                {
                    new ExpertProfileDTO
                    {
                        Email = "eddy.mahlum@nomy.com",
                        FirstName = "Eddy",
                        LastName = "Mahlum",
                        ExpertiseSubAreas = new List<string> { "Financial accounting", "Tax accounting" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "laci.higuera@nomy.com",
                        FirstName = "Laci",
                        LastName = "Higuera",
                        ExpertiseSubAreas = new List<string> { "Governmental accounting", "Management accounting", "Auditing" }
                    },
                    new ExpertProfileDTO
                    {
                        Email = "yuriko.cecil@nomy.com",
                        FirstName = "Yuriko",
                        LastName = "Cecil",
                        ExpertiseSubAreas = new List<string> { "Public accounting", "Cost accounting" }
                    }
                }
            },
            new ExpertiseAreaDTO
            {
                Name = "Psychics",
                ExpertProfiles = new List<ExpertProfileDTO>
                {
                    new ExpertProfileDTO
                    {
                        Email = "marc.nuno@nomy.com",
                        FirstName = "Marc",
                        LastName = "Nuno",
                        ExpertiseSubAreas = new List<string> {"Tarot", "Astrology"}
                    },
                    new ExpertProfileDTO
                    {
                        Email = "madonna.weick@nomy.com",
                        FirstName = "Madonna",
                        LastName = "Weick",
                        ExpertiseSubAreas = new List<string> {"Numerology"}
                    },
                    new ExpertProfileDTO
                    {
                        Email = "florencia.fagg@nomy.com",
                        FirstName = "Florencia",
                        LastName = "Fagg",
                        ExpertiseSubAreas = new List<string> {"Tarot", "Medium"}
                    }
                }
            }
        };

        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly IDataAccessService _dataAccessService;
        private readonly IRapydService _rapydSevice;
        private readonly IPayoutsService _payoutsService;
        private readonly INomyContext _nomyContext;
        private readonly INomyConfig _nomyConfig;
        private readonly ILogger _logger;

        public NomyContextInitializer(IO10ApiGateway o10ApiGateway,
                                      IConfigurationService configurationService,
                                      IDataAccessService dataAccessService,
                                      ILoggerService loggerService,
                                      IRapydService rapydSevice,
                                      IPayoutsService payoutsService,
                                      INomyContext nomyContext)
        {
            _o10ApiGateway = o10ApiGateway;
            _dataAccessService = dataAccessService;
            _rapydSevice = rapydSevice;
            _payoutsService = payoutsService;
            _nomyContext = nomyContext;
            _nomyConfig = configurationService.Get<INomyConfig>();
            _logger = loggerService.GetLogger(nameof(NomyContextInitializer));
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Normal;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            await InitializeIdentityProvider();

            await InitNomyWallet(cancellationToken);

            _logger.Debug("Initializing demo user and experts pre-setup");

            await InitDemoUser(cancellationToken);

            //await InitExperts(cancellationToken);
        }

        private async Task InitExperts(CancellationToken cancellationToken)
        {
            var expertiseAreas = await _dataAccessService.GetExpertiseAreas();

            foreach (var expertiseArea in _expertiseAreas)
            {
                _logger.Debug($"Initializing expertise area {expertiseArea.Name} started");
                try
                {
                    var expertiseAreaPoco = expertiseAreas.FirstOrDefault(s => s.Name == expertiseArea.Name);
                    if (expertiseAreaPoco == null)
                    {
                        expertiseAreaPoco = await _dataAccessService.AddExpertiseArea(expertiseArea.Name, "");
                    }

                    var expertiseSubAreas = await _dataAccessService.GetExpertiseSubAreas(expertiseAreaPoco.ExpertiseAreaId);

                    foreach (var expertProfile in expertiseArea.ExpertProfiles)
                    {
                        _logger.Debug($"Initializing expert profile {expertProfile.Email}");

                        try
                        {
                            foreach (var expertiseSubAreaName in expertProfile.ExpertiseSubAreas)
                            {
                                if (expertiseSubAreas.All(s => s.Name != expertiseSubAreaName))
                                {
                                    await _dataAccessService.AddExpertiseSubArea(expertiseAreaPoco.ExpertiseAreaId, expertiseSubAreaName, "");
                                }
                            }

                            var userExpert = await _dataAccessService.FindUser(expertProfile.Email, cancellationToken);
                            if (userExpert == null)
                            {
                                _logger.Debug($"No user found for expert profile {expertProfile.Email}, creating Rapyd Wallet...");
                                string walletId = await _rapydSevice.CreateRapydWallet(new UserDTO
                                {
                                    Email = expertProfile.Email,
                                    FirstName = expertProfile.FirstName,
                                    LastName = expertProfile.LastName
                                });
                                _logger.Debug($"Expert profile {expertProfile.Email} now has Rapyd Wallet with is {walletId}");

                                string beneficiaryId = await _rapydSevice.CreateBenificiary(new UserDTO
                                {
                                    Email = expertProfile.Email,
                                    FirstName = expertProfile.FirstName,
                                    LastName = expertProfile.LastName
                                });

                                var account = await _o10ApiGateway.FindAccount(expertProfile.Email);
                                bool needToRequestId = false;
                                if (account == null)
                                {
                                    _logger.Debug($"No O10 account found for expert profile {expertProfile.Email}, creating O10 account...");
                                    account = await _o10ApiGateway.RegisterUser(expertProfile.Email, "qqq");
                                    _logger.Debug($"O10 account with id {account.AccountId} was created for {expertProfile.Email}");
                                    needToRequestId = true;
                                }

                                _logger.Debug("Authenticate O10 account...");
                                account = await _o10ApiGateway.Authenticate(account.AccountId, "qqq");

                                if (needToRequestId)
                                {
                                    _logger.Debug($"Requesting O10 identity for expert profile {expertProfile.Email}");
                                    try
                                    {
                                        var attributeValues = await _o10ApiGateway.RequestIdentity(account.AccountId, "qqq", expertProfile.Email, expertProfile.FirstName, expertProfile.LastName, walletId);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.Error($"Failed to request identity for {expertProfile.Email}", ex);
                                    }
                                }

                                userExpert = await _dataAccessService.CreateUser(account.AccountId,
                                                                                 expertProfile.Email,
                                                                                 expertProfile.FirstName,
                                                                                 expertProfile.LastName,
                                                                                 walletId,
                                                                                 beneficiaryId,
                                                                                 null,
                                                                                 cancellationToken);

                                await _dataAccessService.AddExpertProfile(userExpert.NomyUserId, expertProfile.Description ?? string.Empty, (ulong)new Random().Next(10, 20), expertProfile.ExpertiseSubAreas.ToArray());
                            }
                        }
                        catch (AggregateException ex)
                        {
                            _logger.Error($"Error during intializing the expert profile {expertProfile.Email}", ex.InnerException);
                            throw;
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Error during intializing the expert profile {expertProfile.Email}", ex);
                            throw;
                        }
                    }
                }
                finally
                {
                    _logger.Debug($"Initializing expertise area {expertiseArea.Name} finished");
                }
            }
        }

        private async Task InitDemoUser(CancellationToken cancellationToken)
        {
            O10AccountDTO? account;
            var user = await _dataAccessService.FindUser(_demoUser.Email, cancellationToken);
            if (user == null)
            {
                _logger.Debug($"No user found for demo user {_demoUser.Email}, creating Rapyd Wallet...");
                string walletId = await _rapydSevice.CreateRapydWallet(_demoUser);
                string senderId = await _rapydSevice.CreateSender(_demoUser);
                _logger.Debug($"Demo user profile {_demoUser.Email} now has Rapyd Wallet with is {walletId}");

                account = await _o10ApiGateway.FindAccount(_demoUser.Email);
                if (account == null)
                {
                    _logger.Debug($"No O10 account found for demo profile {_demoUser.Email}, creating O10 account...");
                    account = await _o10ApiGateway.RegisterUser(_demoUser.Email, "qqq");
                    _logger.Debug($"O10 account with id {account.AccountId} was created for {_demoUser.Email}");
                }

                user = await _dataAccessService.CreateUser(account.AccountId,
                                                           _demoUser.Email,
                                                           _demoUser.FirstName,
                                                           _demoUser.LastName,
                                                           walletId,
                                                           "",
                                                           senderId,
                                                           cancellationToken);
            } 
            else
            {
                account = await _o10ApiGateway.GetAccount(user.O10Id);
            }

            _logger.Debug("Authenticate O10 account...");
            account = await _o10ApiGateway.Authenticate(account.AccountId, "qqq");

            var attrs = await _o10ApiGateway.GetUserAttributes(account.AccountId);
            if (attrs == null || attrs.Count == 0)
            {
                _logger.Debug($"Requesting O10 identity for expert profile {_demoUser.Email}");
                try
                {
                    var attributeValues = await _o10ApiGateway.RequestIdentity(account.AccountId, "qqq", _demoUser.Email, _demoUser.FirstName, _demoUser.LastName, user.WalletId);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Failed to request identity for {_demoUser.Email}", ex);
                }
            }

            await _rapydSevice.ReplenishFunds(user.WalletId, 500, 1000);
        }

        private async Task InitializeIdentityProvider()
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
                await _o10ApiGateway.Authenticate(_nomyContext.O10IdentityProvider.AccountId, "qqq");

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

        private async Task InitNomyWallet(CancellationToken cancellationToken)
        {
            var nomyWalletParam = await _dataAccessService.GetSystemParameter("NomyWallet", cancellationToken);
            var nomyBeneficiaryParam = await _dataAccessService.GetSystemParameter("NomyBeneficiary", cancellationToken);
            var nomySenderParam = await _dataAccessService.GetSystemParameter("NomySender", cancellationToken);

            if (nomyWalletParam == null)
            {
                string walletId = await _rapydSevice.CreateRapydWallet(new UserDTO { Email = "info@nomy.com", FirstName = "Nomy LTD", LastName = "" });
                nomyWalletParam = await _dataAccessService.SetSystemParameter("NomyWallet", walletId, cancellationToken);
            }

            var nomyUser = new UserDTO
            {
                FirstName = "Nomy LTD",
                LastName = "",
                Email = "info@nomy.com"
            };

            if (nomyBeneficiaryParam == null)
            {
                var beneficiary = await _rapydSevice.CreateBenificiary(nomyUser);

                nomyBeneficiaryParam = await _dataAccessService.SetSystemParameter("NomyBeneficiary", beneficiary, cancellationToken);
            }

            if (nomySenderParam == null)
            {
                var sender = await _rapydSevice.CreateSender(nomyUser);

                nomySenderParam = await _dataAccessService.SetSystemParameter("NomySender", sender, cancellationToken);
            }

            _payoutsService.Initialize(nomyWalletParam.Value, nomyBeneficiaryParam.Value, nomySenderParam.Value);
        }
    }
}
