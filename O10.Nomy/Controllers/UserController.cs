﻿using Microsoft.AspNetCore.Mvc;
using O10.Nomy.DTOs;
using O10.Nomy.Rapyd;
using System.Threading.Tasks;
using Flurl.Http;
using O10.Nomy.Services;
using System.Threading;
using O10.Nomy.Hubs;
using Microsoft.AspNetCore.SignalR;
using O10.Client.Web.DataContracts.User;
using System.Collections.Generic;
using O10.Nomy.DemoFeatures;
using O10.Nomy.DemoFeatures.Models;
using O10.Client.Web.DataContracts;
using System.Linq;
using O10.Client.DataLayer.AttributesScheme;

namespace O10.Nomy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRapydApi _rapydApi;
        private readonly IO10ApiGateway _apiGateway;
        private readonly IPaymentSessionsService _paymentSessionsService;
        private readonly ISessionsPool _sessionsPool;
        private readonly IDataAccessService _dataAccessService;
        private readonly IHubContext<ChatSessionHub> _hubContext;

        public UserController(IRapydApi rapydApi,
                              IO10ApiGateway o10ApiGateway,
                              IPaymentSessionsService paymentSessionsService,
                              ISessionsPool sessionsPool,
                              IHubContext<ChatSessionHub> hubContext,
                              IDataAccessService dataAccessService)
        {
            _rapydApi = rapydApi;
            _apiGateway = o10ApiGateway;
            _paymentSessionsService = paymentSessionsService;
            _sessionsPool = sessionsPool;
            _dataAccessService = dataAccessService;
            _hubContext = hubContext;
        }

        [HttpGet("{accountId}/attributes")]
        public async Task<IActionResult> GetUserAttributes(long accountId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(await _apiGateway.GetUserAttributes(user.Account.O10Id));
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
            return Ok(await _apiGateway.GetUserActionInfo(actionInfo));
        }

        [HttpGet("{accountId}/Details")]
        public async Task<ActionResult<UserAccountDetailsDto>> GetUserAccountDetails(long accountId, CancellationToken cancellationToken)
        {
            var user = await _dataAccessService.GetUser(accountId, cancellationToken);

            return Ok(await _apiGateway.GetUserAccountDetails(user.Account.O10Id));
        }

        [HttpPost("{accountId}/Compromized")]
        public async Task<IActionResult> SendCompromizedClaim(long accountId, UnauthorizedUseDto unauthorizedUse, CancellationToken cancellationToken)
        {
            var user = await _dataAccessService.GetUser(accountId, cancellationToken);

            await _apiGateway.SendCompromizationClaim(user.Account.O10Id, unauthorizedUse);

            return Ok();
        }

        [HttpGet("{accountId}/Secrets")]
        public async Task<ActionResult<QrCodeDto>> GetDiscloseSecretsCode(long accountId, string password, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(await _apiGateway.GetDiscloseSecretsCode(user.Account.O10Id, password));
        }

        [HttpGet("{accountId}/ActionDetails")]
        public async Task<IActionResult> GetActionDetails(long accountId, string actionInfo, long userAttributeId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            return Ok(await _apiGateway.GetActionDetails(user.Account.O10Id, actionInfo, userAttributeId));
        }

        [HttpPost("{accountId}/UniversalProofs")]
        public async Task<IActionResult> SendUniversalProofs(long accountId, [FromBody] UniversalProofsSendingRequest universalProofs, CancellationToken ct)
        {
            var user = await _dataAccessService.GetUser(accountId, ct);

            _sessionsPool.Push(new UserSessionInfo { SessionKey = universalProofs.SessionKey, UserId = accountId });

            universalProofs.IdentityPools = new List<UniversalProofsSendingRequest.IdentityPool>
            {
                new UniversalProofsSendingRequest.IdentityPool
                {
                    RootAttributeId = universalProofs.RootAttributeId
                }
            };

            await _apiGateway.SendUniversalProofs(user.Account.O10Id, universalProofs);

            return Ok();
        }

        [HttpPost("{accountId}/attributes")]
        public async Task<IActionResult> RequestIdentity(long accountId, [FromBody] RequestIdentityDto requestIdentity, CancellationToken cancellationToken)
        {
            var user = await _dataAccessService.GetUser(accountId, cancellationToken);

            var attributeValues = await _apiGateway.RequestIdentity(user.Account.O10Id,
                                                                    requestIdentity.Password,
                                                                    requestIdentity.AttributeScheme.RootAttributeContent,
                                                                    requestIdentity.AttributeScheme.AssociatedSchemes.First().Attributes.FirstOrDefault(a => a.SchemeName == AttributesSchemes.ATTR_SCHEME_NAME_FIRSTNAME)?.Content,
                                                                    requestIdentity.AttributeScheme.AssociatedSchemes.First().Attributes.FirstOrDefault(a => a.SchemeName == AttributesSchemes.ATTR_SCHEME_NAME_LASTNAME)?.Content,
                                                                    requestIdentity.AttributeScheme.AssociatedSchemes.First().Attributes.FirstOrDefault(a => a.SchemeName == "RapydWalletId")?.Content);

            return Ok();
        }
    }
}
