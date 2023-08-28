using System.Threading.Tasks;
using MealPlan.API.Requests.Versions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        private readonly IMediator _mediator;

        public VersionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-version")]
        public async Task<ActionResult<string>> GetVersion([FromQuery] GetVersionRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }
    }
}
