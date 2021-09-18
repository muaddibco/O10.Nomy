using Microsoft.AspNetCore.Mvc;
using O10.Client.Web.DataContracts.User;
using O10.Core.ExtensionMethods;
using O10.Core.Translators;
using O10.Crypto.ConfidentialAssets;
using O10.Nomy.DTOs;
using O10.Nomy.Models;
using O10.Nomy.Rapyd;
using O10.Nomy.Services;
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
        private readonly IRapydService _rapydSevice;
        private readonly ITranslatorsRepository _translatorsRepository;
        private readonly IDataAccessService _dataAccessService;

        public AccountsController(IO10ApiGateway apiGateway,
                                  IRapydApi rapydApi,
                                  IRapydService rapydSevice,
                                  ITranslatorsRepository translatorsRepository,
                                  IDataAccessService dataAccessService)
        {
            _apiGateway = apiGateway;
            _rapydApi = rapydApi;
            _rapydSevice = rapydSevice;
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

        [HttpPost("{accountId}/Auth")]
        public async Task<IActionResult> Authenticate(long accountId, [FromBody] AuthenticateAccountDTO authenticateAccount, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);
            var account = await _apiGateway.Authenticate(user.Account.O10Id, authenticateAccount.Password);

            return Ok(account);
        }

        [HttpGet("{accountId}/Auth")]
        public async Task<IActionResult> IsAuthenticated(long accountId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);
            return Ok(new { IsAuthenticated = await _apiGateway.IsAuthenticated(user.Account.O10Id, ct)});
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO user, CancellationToken cancellationToken)
        {
            var account = await _apiGateway.RegisterUser(user.Email, user.Password);
            account = await _apiGateway.Authenticate(account.AccountId, user.Password);

            string walletId = await _rapydSevice.CreateRapydWallet(user);
            await _rapydSevice.ReplenishFunds(walletId, -1, 1000);
            string beneficiaryId = await _rapydSevice.CreateBenificiary(user);
            string senderId = await _rapydSevice.CreateSender(user);

            if (!user.IsEmptyOnly)
            {
                var attributeValues = await _apiGateway.RequestIdentity(account.AccountId, user.Password, user.Email, user.FirstName, user.LastName, walletId);
            }

            var userPoco = await _dataAccessService.CreateUser(account.AccountId,
                                                               user.Email,
                                                               user.FirstName,
                                                               user.LastName,
                                                               walletId,
                                                               beneficiaryId,
                                                               senderId,
                                                               cancellationToken);

            return Ok(_translatorsRepository.GetInstance<NomyUser, UserDetailsDTO>().Translate(userPoco));
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> OverrideAccount(long accountId, DisclosedSecretsDto disclosedSecrets, CancellationToken cancellationToken)
        {
            var user = await _dataAccessService.GetUser(accountId, cancellationToken);

            var sourceO10Account = await _apiGateway.FindAccountByKeys(disclosedSecrets);
            var sourceNomyAccount = await _dataAccessService.GetUserByO10Id(sourceO10Account.AccountId, cancellationToken);

            await _apiGateway.OverrideAccount(user.Account.O10Id, disclosedSecrets);

            var newO10User = await _apiGateway.DuplicateAccount(sourceO10Account.AccountId, user.Account.O10Id);

            var newUser = await _dataAccessService.DuplicateUser(sourceNomyAccount.Account.NomyAccountId, user.Account.NomyAccountId, cancellationToken);

            await _apiGateway.StopAccount(user.Account.O10Id);
            await _apiGateway.Authenticate(user.Account.O10Id, disclosedSecrets.Password);

            return Ok();
        }


        [HttpPost("{accountId}/Reset")]
        public async Task<IActionResult> ResetAccount(long accountId, AuthenticateAccountDTO authenticateAccount, CancellationToken cancellationToken)
        {
            var user = await _dataAccessService.GetUser(accountId, cancellationToken);

            var account = await _apiGateway.ResetAccount(accountId, authenticateAccount.Password);

            var userNew = await _dataAccessService.UpdateO10Id(accountId, account.AccountId, cancellationToken);

            var attributeValues = await _apiGateway.RequestIdentity(account.AccountId, authenticateAccount.Password, userNew.Email, userNew.FirstName, userNew.LastName, userNew.WalletId);

            return Ok();
        }
    }
}
