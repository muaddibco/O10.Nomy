using Microsoft.AspNetCore.Mvc;
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
    public class PaymentsController : ControllerBase
    {
        private readonly IPayoutsService _payoutsService;

        public PaymentsController(IPayoutsService payoutsService)
        {
            _payoutsService = payoutsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayout(CancellationToken cancellationToken)
        {
            await _payoutsService.CreatePayout(cancellationToken);

            return Ok();
        }
    }
}
