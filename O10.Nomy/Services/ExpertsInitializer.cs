using Flurl.Http;
using O10.Core;
using O10.Core.Architecture;
using O10.Core.Logging;
using O10.Nomy.DTOs;
using O10.Nomy.Rapyd;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Singleton)]
    public class ExpertsInitializer : InitializerBase
    {
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
        private readonly IDataAccessService _dataAccessService;
        private readonly IO10ApiGateway _apiGateway;
        private readonly IRapydApi _rapydApi;
        private readonly ILogger _logger;
        public ExpertsInitializer(IDataAccessService dataAccessService, IO10ApiGateway o10ApiGateway, IRapydApi rapydApi, ILoggerService loggerService)
        {
            _dataAccessService = dataAccessService;
            _apiGateway = o10ApiGateway;
            _rapydApi = rapydApi;
            _logger = loggerService.GetLogger(nameof(ExpertsInitializer));
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Lowest;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            _logger.Debug("Initializing experts pre-setup");
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

                        foreach (var expertiseSubAreaName in expertProfile.ExpertiseSubAreas)
                        {
                            if (expertiseSubAreas.All(s => s.Name != expertiseSubAreaName))
                            {
                                await _dataAccessService.AddExpertiseSubArea(expertiseAreaPoco.ExpertiseAreaId, expertiseSubAreaName, "");
                            }
                        }

                        var user = await _dataAccessService.FindUser(expertProfile.Email, cancellationToken);
                        if (user == null)
                        {
                            _logger.Debug($"No user found for expert profile {expertProfile.Email}, creating Rapyd Wallet...");
                            string walletId = await CreateRapydWallet(new UserDTO
                            {
                                Email = expertProfile.Email,
                                FirstName = expertProfile.FirstName,
                                LastName = expertProfile.LastName
                            });
                            _logger.Debug($"Expert profile {expertProfile.Email} now has Rapyd Wallet with is {walletId}");

                            var account = await _apiGateway.FindAccount(expertProfile.Email);
                            bool needToRequestId = false;
                            if (account == null)
                            {
                                _logger.Debug($"No O10 account found for expert profile {expertProfile.Email}, creating O10 account...");
                                account = await _apiGateway.RegisterUser(expertProfile.Email, "qqq");
                                needToRequestId = true;
                            }

                            account = await _apiGateway.Start(account.AccountId);
                            await _apiGateway.SetBindingKey(account.AccountId, "qqq");

                            if (needToRequestId)
                            {
                                _logger.Debug($"Requesting O10 identity for expert profile {expertProfile.Email}");
                                var attributeValues = await _apiGateway.RequestIdentity(account.AccountId, "qqq", expertProfile.Email, expertProfile.FirstName, expertProfile.LastName, walletId);
                            }

                            user = await _dataAccessService.CreateUser(account.AccountId, expertProfile.Email, expertProfile.FirstName, expertProfile.LastName, walletId, cancellationToken);

                            await _dataAccessService.AddExpertProfile(user.NomyUserId, expertProfile.Description ?? string.Empty, (ulong)new Random().Next(10, 20), expertProfile.ExpertiseSubAreas.ToArray());
                        }
                    }
                }
                finally
                {
                    _logger.Debug($"Initializing expertise area {expertiseArea.Name} finished");
                }
            }
        }

        private async Task<string> CreateRapydWallet(UserDTO user)
        {
            try
            {
                WalletContact walletContact = new()
                {
                    ContactType = ContactType.Business,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                WalletRequestDTO walletRequest = new()
                {
                    Category = WalletCategory.General,
                    Contact = walletContact,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EwalletReferenceId = Guid.NewGuid().ToString(),
                    Type = WalletType.Company
                };

                var response = await _rapydApi.CreateWallet(walletRequest);

                string walletId = response.Id;
                return walletId;
            }
            catch(FlurlHttpException ex)
            {
                string reason = await ex.Call.Response.GetStringAsync();
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
