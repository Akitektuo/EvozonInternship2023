using MealPlan.API.Middleware;
using MealPlan.API.Requests.Users;
using MealPlan.API.Requests.Versions;
using MealPlan.Business.Users.Models;
using MealPlan.Business.Users.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());
            
            return Ok();
    	}

        [HttpPost("login")]
        public async Task<ActionResult<UserDetails>> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin, Role.User)]
        [HttpGet("get-wallet")]
        public async Task<ActionResult<double>> GetWallet()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _mediator.Send(new GetWalletQuery { Email = email });

            return Ok(result);
        }
    }
}