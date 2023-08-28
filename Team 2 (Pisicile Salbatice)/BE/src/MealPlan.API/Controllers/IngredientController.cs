using MealPlan.API.Authorization;
using MealPlan.API.Requests.Ingredients;
using MealPlan.Business.Ingredients.Models;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MealPlan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("get-ingredients")]
        public async Task<ActionResult<GetIngredientsModel>> GetIngredients()
        {
            var result = await _mediator.Send(new GetIngredientsQuery());

            return Ok(result);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost("add-ingredient")]
        public async Task<ActionResult> AddIngredient([FromBody] AddIngredientRequest request)
        {
            await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}
