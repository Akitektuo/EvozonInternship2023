using MealPlan.API.Middleware;
using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RolesAuth(Role.Admin, Role.User)]
        [HttpGet("get-recipe/{id}")]
        public async Task<ActionResult<RecipeDetails>> GetRecipe([FromRoute] GetRecipeRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("get-unused-recipes")]
        public async Task<ActionResult<PaginationModel<RecipeOverview>>> GetAllRecipes([FromBody] GetUnusedRecipesRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("add-recipe")]
        public async Task<ActionResult> AddRecipe([FromBody] AddRecipeRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}