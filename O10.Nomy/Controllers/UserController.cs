using Microsoft.AspNetCore.Mvc;
using O10.Nomy.DTOs;
using O10.Nomy.Rapyd;
using System.Threading.Tasks;
using Flurl.Http;
using O10.Nomy.Services;
using System.Threading;
using O10.Nomy.Hubs;
using Microsoft.AspNetCore.SignalR;
using O10.Client.Web.DataContracts.User;

namespace O10.Nomy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRapydApi _rapydApi;
        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly IPaymentSessionsService _paymentSessionsService;
        private readonly IDataAccessService _dataAccessService;
        private readonly IHubContext<ChatSessionHub> _hubContext;

        public UserController(IRapydApi rapydApi,
                              IO10ApiGateway o10ApiGateway,
                              IPaymentSessionsService paymentSessionsService,
                              IHubContext<ChatSessionHub> hubContext,
                              IDataAccessService dataAccessService)
        {
            _rapydApi = rapydApi;
            _o10ApiGateway = o10ApiGateway;
            _paymentSessionsService = paymentSessionsService;
            _dataAccessService = dataAccessService;
            _hubContext = hubContext;
        }

        [HttpGet("{accountId}/attributes")]
        public async Task<IActionResult> GetUserAttributes(long accountId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(await _o10ApiGateway.GetUserAttributes(user.Account.O10Id));
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetUserDetails(long accountId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(user);
        }

        [HttpPost("Session/{sessionId}")]
        public IActionResult ConfirmSession(string sessionId, CancellationToken ct)
        {
            _hubContext.Clients.Group(sessionId).SendAsync("Confirmed", new { sessionId }, ct);

            return Ok();
        }

        [HttpPost("{accountId}/Invoice")]
        public async Task<IActionResult> SendInvoice(long accountId, [FromBody] InvoiceDTO invoice, CancellationToken ct)
        {
            var invoiceEntry = await _paymentSessionsService.PushInvoice(accountId, invoice.SessionId, invoice.Currency, invoice.Amount, ct);

            return Ok(invoiceEntry);
        }

        [HttpPost("{accountId}/Pay")]
        public async Task<IActionResult> PayInvoice(long accountId, [FromBody] PayInvoiceDTO payInvoice, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            await _rapydApi.PutFundsOnHold(user.WalletId, payInvoice.Currency, payInvoice.Amount);
            var paymentEntry = await _paymentSessionsService.PushPayment(accountId, payInvoice.SessionId, payInvoice.InvoiceCommitment, payInvoice.Currency, payInvoice.Amount, ct);

            return Ok(paymentEntry);
        }

        [HttpGet("ActionInfo")]
        public async Task<IActionResult> GetUserActionInfo(string actionInfo)
        {
            return Ok(await _o10ApiGateway.GetUserActionInfo(actionInfo));
        }

        [HttpGet("{accountId}/ActionDetails")]
        public async Task<IActionResult> GetActionDetails(long accountId, string actionInfo, long userAttributeId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(await _o10ApiGateway.GetActionDetails(user.Account.O10Id, actionInfo, userAttributeId));
        }
    }
}
