using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O10.Core.Configuration;
using O10.Nomy.Configuration;
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
        private readonly INomyConfig _nomyConfig;

        public JointServiceController(IJointPurchasesService jointPurchasesService, IConfigurationService configurationService)
        {
            _jointPurchasesService = jointPurchasesService;
            _nomyConfig = configurationService.Get<INomyConfig>();
        }

        [HttpGet]
        public async Task<ActionResult<NomyAccountDTO>> GetJointServiceAccount()
        {
            return Ok(new NomyAccountDTO { AccountId = (await _jointPurchasesService.GetJointServiceRecord()).Account.NomyAccountId });
        }

        [HttpGet("O10Hub")]
        public IActionResult GetO10HubUri()
        {
            return Ok(new { O10HubUri = _nomyConfig.HubUri });
        }
    }
}
