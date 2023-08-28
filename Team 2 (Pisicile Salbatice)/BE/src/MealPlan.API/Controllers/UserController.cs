using MealPlan.API.Requests.Users;
using MealPlan.API.Services.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticationService _service;

        public UserController(IMediator mediator, IAuthenticationService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());
            if (result != null) 
            {
                result = _service.GenerateJwt(result);
            }

            return Ok(result);
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}
