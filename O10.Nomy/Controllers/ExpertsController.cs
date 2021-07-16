using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using O10.Nomy.DTOs;
using O10.Nomy.Hubs;
using O10.Nomy.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IDataAccessService _dataAccessService;
        private readonly IPaymentSessionsService _paymentSessionsService;
        private readonly IHubContext<ChatSessionHub> _hubContext;

        public ExpertsController(IDataAccessService dataAccessService, IPaymentSessionsService paymentSessionsService, IHubContext<ChatSessionHub> hubContext)
        {
            _dataAccessService = dataAccessService;
            _paymentSessionsService = paymentSessionsService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperts()
        {
            var expertiseAreas = await _dataAccessService.GetExpertiseAreas();
            var res = await Task.WhenAll(
                expertiseAreas.Select(
                    async s => new ExpertiseAreaDTO
                    {
                        Name = s.Name,
                        ExpertProfiles = (
                                await _dataAccessService.GetExpertProfiles(s.ExpertiseSubAreas.Select(a => a.Name).ToArray())
                            ).Select(p => 
                                new ExpertProfileDTO
                                {
                                    UserId = p.NomyUser.NomyUserId,
                                    ExpertProfileId = p.ExpertProfileId,
                                    Email = p.NomyUser.Email,
                                    FirstName = p.NomyUser.FirstName,
                                    LastName = p.NomyUser.LastName,
                                    Description = p.Description,
                                    Fee = p.Fee,
                                    ExpertiseSubAreas = p.ExpertiseSubAreas.Select(a => a.Name).ToList(),
                                    WalletId = p.NomyUser.WalletId
                                }).ToList()
                    }));

            return Ok(res);
        }

        [HttpGet("{expertProfileId}")]
        public async Task<IActionResult> GetExpert(long expertProfileId)
        {
            var expertProfile = await _dataAccessService.GetExpertProfile(expertProfileId);

            return Ok(new ExpertProfileDTO
            {
                UserId = expertProfile.NomyUser.NomyUserId,
                ExpertProfileId = expertProfile.ExpertProfileId,
                Email = expertProfile.NomyUser.Email,
                FirstName = expertProfile.NomyUser.FirstName,
                LastName = expertProfile.NomyUser.LastName,
                Description = expertProfile.Description,
                Fee = expertProfile.Fee,
                ExpertiseSubAreas = expertProfile.ExpertiseSubAreas.Select(a => a.Name).ToList(),
                WalletId = expertProfile.NomyUser.WalletId
            });
        }

        [HttpPost("Session")]
        public IActionResult InitiateSession(CancellationToken ct)
        {
            string sessionId = _paymentSessionsService.CreatePaymentsSession();

            return Ok(new { sessionId });
        }

        [HttpPost("{expertProfileId}/Session/{sessionId}")]
        public async Task<IActionResult> StartSession(long expertProfileId, string sessionId)
        {
            var expertProfile = await _dataAccessService.GetExpertProfile(expertProfileId);

            await _hubContext.Clients.Group(expertProfile.NomyUser.NomyUserId.ToString()).SendAsync("Start", new { sessionId, expertProfileId });

            return Ok();
        }

        [HttpPost("{expertProfileId}/Chat")]
        public async Task<IActionResult> SendChatInvitation(long expertProfileId, string sessionId)
        {
            var expertProfile = await _dataAccessService.GetExpertProfile(expertProfileId);

            await _hubContext.Clients.Group(expertProfile.NomyUser.NomyUserId.ToString()).SendAsync("Invitation", new { sessionId, expertProfileId });

            return Ok();
        }
    }
}
