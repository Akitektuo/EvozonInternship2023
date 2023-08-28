using MealPlan.API.Authorization;
using MealPlan.API.Requests.Menus;
using MealPlan.API.Requests.Menus.AddGeneratedMenu;
using MealPlan.API.Requests.Recipes;
using MealPlan.API.Requests.Users;
using MealPlan.Business.Menus.Models;
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

        [HttpPost("get-all-menus")]
        public async Task<ActionResult<GetAllMenusModel>> GetAllMenus([FromBody] GetAllMenusRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("add-menu")]
        public async Task<ActionResult<bool>> AddMenu([FromBody] AddMenuRequest request)
        {
            await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [HttpGet("get-menu/{menuId}")]
        public async Task<ActionResult<GetMenuDetailsModel>> GetMenu([FromRoute] GetMenuRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("add-generated-menu")]
        public async Task<ActionResult> AddGeneratedMenu([FromBody] AddGeneratedMenuRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("get-generated-menu")]
        public async Task<ActionResult<GeneratedMenuModel>> GenerateMenuSuggestion([FromQuery] GetGeneratedMenuRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());
            
            return Ok(result);
        }
    }
}
