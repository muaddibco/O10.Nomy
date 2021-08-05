using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O10.Nomy.DTOs;
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
    public class ServiceProvidersController : ControllerBase
    {
        private readonly IO10ApiGateway _o10ApiGateway;
        private readonly IDataAccessService _dataAccessService;

        public ServiceProvidersController(IO10ApiGateway o10ApiGateway, IDataAccessService dataAccessService)
        {
            _o10ApiGateway = o10ApiGateway;
            _dataAccessService = dataAccessService;
        }

        [HttpGet("SessionInfo")]
        public async Task<ActionResult<QrCodeDto>> GetSessionInfo(long accountId, CancellationToken ct)
        {
            var user = await _dataAccessService.GetServiceProvider(accountId, ct);

            return await _o10ApiGateway.GetSessionInfo(user.Account.O10Id);
        }
    }
}
