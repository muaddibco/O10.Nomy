using Flurl.Http;
using O10.Core.Architecture;
using O10.Core.Logging;
using O10.Nomy.DTOs;
using O10.Nomy.Rapyd.DTOs;
using O10.Nomy.Rapyd.DTOs.Disburse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [RegisterDefaultImplementation(typeof(IRapydService), Lifetime = LifetimeManagement.Singleton)]
    public class RapydService : IRapydService
    {
        private readonly IRapydApi _rapydApi;
        private readonly ILogger _logger;

        public RapydService(IRapydApi rapydApi, ILoggerService loggerService)
        {
            _rapydApi = rapydApi;
            _logger = loggerService.GetLogger(nameof(RapydService));
        }

        public async Task<string> CreateBenificiary(UserDTO user)
        {
            try
            {
                BeneficiaryDTO beneficiaryDTO = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Category = BeneficiaryCategory.Bank,
                    EntityType = EntityType.Individual,
                    Country = "US",
                    Currency = "USD",
                    PayoutMethodType = "us_visa_card",
                    Properties = new Dictionary<string, string>
                    {
                        { "email", user.Email },
                        { "card_number", "4462030000000000" },
                        { "card_expiration_month", "11" },
                        { "card_expiration_year", "2025" },
                        { "card_cvv", "111" },
                        { "company_name", "" },
                        { "postcode", "" }
                    }
                };

                var beneficiary = await _rapydApi.CreateBenificiary(beneficiaryDTO);

                return beneficiary.Id;
            }
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to create beneficiary due to the error '{str}'", ex);
                throw;
            }
            catch(Exception ex)
            {
                _logger.Error($"Failed to create beneficiary due to the error", ex);
                throw;
            }
        }

        public async Task<string> CreateRapydWallet(UserDTO user)
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
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to create wallet due to the error '{str}'", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to create wallet due to the error", ex);
                throw;
            }
        }

        public async Task<string> CreateSender(UserDTO user)
        {
            try
            {
                SenderDTO senderDTO = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = "US",
                    Currency = "USD",
                    EntityType = EntityType.Individual
                };

                var sender = await _rapydApi.CreateSender(senderDTO);

                return sender.Id;
            }
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to create sender due to the error '{str}'", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to create sender due to the error", ex);
                throw;
            }
        }

        public async Task<ulong?> ReplenishFunds(string walletId, int threshold, ulong target)
        {
            try
            {
                var wallet = await _rapydApi.GetWallet(walletId);
                if (threshold < 0 || wallet.Accounts.Select(a => int.Parse(a.Balance)).Sum() <= threshold)
                {
                    var depositResponse = await _rapydApi.DepositFunds(walletId, "USD", target);

                    return depositResponse.Amount;
                }

                return ((ulong?)wallet.Accounts?.Sum(a => (decimal)ulong.Parse(a.Balance)));
            }
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to replenish funds due to the error '{str}'", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to replenish funds due to the error", ex);
                throw;
            }
        }

        public async Task TransferFunds(string sourceWalletId, string destinationWalletId, string currency, ulong amount)
        {
            try
            {
                var releaseResp = await _rapydApi.ReleaseFunds(sourceWalletId, currency, amount);
                var resp1 = await _rapydApi.TransferFunds(sourceWalletId, destinationWalletId, currency, amount);
                var resp2 = await _rapydApi.ConfirmTransfer(resp1.Id);
            }
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to transfer funds due to the error '{str}'", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to transfer funds due to the error", ex);
                throw;
            }
        }

        public async Task PayoutFunds(string sourceWalletId, string beneficiaryId, string currency, ulong amount)
        {
            try
            {
                var resp = await _rapydApi.PayoutFunds(sourceWalletId, beneficiaryId, currency, amount);
            }
            catch (FlurlHttpException ex)
            {
                var str = await ex.Call.Response?.ResponseMessage.Content.ReadAsStringAsync();
                _logger.Error($"Failed to payout funds due to the error '{str}'", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to payout funds due to the error", ex);
                throw;
            }
        }
    }
}
