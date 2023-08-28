using MealPlan.API.Authorization;
using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Recipes.Models;
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

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("add-recipe")]
        public async Task<ActionResult> AddRecipe([FromBody] AddRecipeRequest request)
        {
            await _mediator.Send(request.ToCommand());

            return Ok();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("get-recipe/{recipeId}")]
        public async Task<ActionResult<RecipeModel>> GetRecipe([FromRoute] GetRecipeRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("get-recipes")]
        public async Task<ActionResult<GetRecipesModel>> GetRecipes([FromBody] GetRecipesRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }
    }
}
