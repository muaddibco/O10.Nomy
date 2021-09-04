using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using O10.Core.Configuration;
using O10.Nomy.Configuration;
using O10.Nomy.DTOs;
using O10.Nomy.JointPurchases;
using O10.Nomy.JointPurchases.Models;
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
        public async Task<ActionResult<NomyAccountDTO>> GetJointServiceAccount(CancellationToken cancellationToken)
        {
            return Ok(new NomyAccountDTO { AccountId = (await _jointPurchasesService.GetJointServiceRecord(cancellationToken)).Account.NomyAccountId });
        }

        [HttpGet("O10Hub")]
        public IActionResult GetO10HubUri()
        {
            return Ok(new { O10HubUri = _nomyConfig.HubUri });
        }

        [HttpPost("{o10RegistrationId}/JointGroup")]
        public async Task<ActionResult<JointGroupDTO>> AddJointGroup(long o10RegistrationId, [FromBody] AddJointGroupRequestDTO addJointGroupRequest, CancellationToken cancellationToken)
        {
            return await _jointPurchasesService.AddJointGroup(o10RegistrationId, addJointGroupRequest.Name, addJointGroupRequest.Description, cancellationToken);
        }

        [HttpGet("{o10RegistrationId}/JointGroups")]
        public async Task<ActionResult<List<JointGroupDTO>>> GetJointGroups(long o10RegistrationId, CancellationToken cancellationToken)
        {
            return await _jointPurchasesService.GetJointGroups(o10RegistrationId, cancellationToken);
        }

        [HttpPost("JointGroup/{groupId}/Member")]
        public async Task<ActionResult<JointGroupMemberDTO>> AddJointGroupMember(long groupId, [FromBody] JointGroupMemberDTO jointGroupMember, CancellationToken cancellationToken)
        {
            return await _jointPurchasesService.AddJointGroupMember(groupId, jointGroupMember.Email, jointGroupMember.Description, cancellationToken);
        }

        [HttpGet("JointGroup/{groupId}/Members")]
        public async Task<ActionResult<List<JointGroupMemberDTO>>> GetJointGroupMembers(long groupId, CancellationToken cancellationToken)
        {
            return await _jointPurchasesService.GetJointGroupMembers(groupId, cancellationToken);
        }
    }
}
