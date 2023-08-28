using MealPlan.API.Middleware;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RolesAuth(Role.Admin, Role.User)]
        [HttpPost("get-all-menus")]
        public async Task<ActionResult<PaginationModel<MenuOverview>>> GetAllMenus([FromBody] GetAllMenusRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("add-menu")]
        public async Task<ActionResult> AddMenu([FromBody] AddMenuRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("add-suggested-menu")]
        public async Task<ActionResult> AddSuggestedMenu([FromBody] AddSuggestedMenuRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("get-suggested-menu")]
        public async Task<ActionResult<SuggestedMenu>> GetSuggestedMenu([FromBody] GetSuggestedMenuRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }
        
        [RolesAuth(Role.Admin, Role.User)]
        [HttpGet("get-menu/{Id}")]
        public async Task<ActionResult<MenuDetails>> GetMenu([FromRoute] GetMenuRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }
    }
}