using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O10.Core.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.JointPurchases;
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
    public class JointServiceController : ControllerBase
    {
        private readonly IJointPurchasesService _jointPurchasesService;

        public JointServiceController(IJointPurchasesService jointPurchasesService)
        {
            _jointPurchasesService = jointPurchasesService;
        }

        [HttpGet]
        public async Task<ActionResult<NomyAccountDTO>> GetJointServiceAccount(CancellationToken cancellationToken)
        {
            return Ok(new NomyAccountDTO { AccountId = (await _jointPurchasesService.GetJointServiceRecord()).Account.NomyAccountId });
        }
    }
}
