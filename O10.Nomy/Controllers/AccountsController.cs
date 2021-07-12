using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O10.Client.Common.Entities;
using O10.Core.Configuration;
using O10.Core.Translators;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.Models;
using O10.Nomy.Rapyd;
using O10.Nomy.Rapyd.DTOs;
using O10.Nomy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IO10ApiGateway _apiGateway;
        private readonly IRapydApi _rapydApi;
        private readonly ITranslatorsRepository _translatorsRepository;
        private readonly IDataAccessService _dataAccessService;

        public AccountsController(IO10ApiGateway apiGateway,
                                  IRapydApi rapydApi,
                                  ITranslatorsRepository translatorsRepository,
                                  IDataAccessService dataAccessService)
        {
            _apiGateway = apiGateway;
            _rapydApi = rapydApi;
            _translatorsRepository = translatorsRepository;
            _dataAccessService = dataAccessService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> FindAccount(long accountId, CancellationToken ct)
        {
            return Ok(_translatorsRepository.GetInstance<NomyUser?, UserDetailsDTO?>().Translate(await _dataAccessService.GetUser(accountId, ct)));
        }

        [HttpGet("Find")]
        public async Task<IActionResult> FindAccount(string accountAlias, CancellationToken ct)
        {
            return Ok(_translatorsRepository.GetInstance<NomyUser?, UserDetailsDTO?>().Translate(await _dataAccessService.FindUser(accountAlias, ct)));
        }

        [HttpPost("{accountId}/Start")]
        public async Task<IActionResult> StartAccount(long accountId, string password, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);
            var account = await _apiGateway.Start(user.O10Id);
            await _apiGateway.SetBindingKey(user.O10Id, password);

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO user, CancellationToken cancellationToken)
        {
            var account = await _apiGateway.RegisterUser(user.Email, user.Password);
            account = await _apiGateway.Start(account.AccountId);
            await _apiGateway.SetBindingKey(account.AccountId, user.Password);

            string walletId = await CreateRapydWallet(user);

            var attributeValues = await _apiGateway.RequestIdentity(account.AccountId, user.Password, user.Email, user.FirstName, user.LastName, walletId);

            var userPoco = await _dataAccessService.CreateUser(account.AccountId, user.Email, user.FirstName, user.LastName, walletId, cancellationToken);

            return Ok(_translatorsRepository.GetInstance<NomyUser, UserDetailsDTO>().Translate(userPoco));
        }

        private async Task<string> CreateRapydWallet(UserDTO user)
        {
            WalletContact walletContact = new()
            {
                ContactType = ContactType.Personal,
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
                Type = WalletType.Person
            };

            var response = await _rapydApi.CreateWallet(walletRequest);

            string walletId = response.Id;

            var depositResponse = await _rapydApi.DepositFunds(walletId, "USD", 1000);

            return walletId;
        }
    }
}
