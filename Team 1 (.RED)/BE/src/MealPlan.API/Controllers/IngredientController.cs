using MealPlan.API.Middleware;
using MealPlan.API.Requests.Ingredients;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Ingredients.Models;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [RolesAuth(Role.Admin)]
        [HttpGet("get-all-ingredients")]
        public async Task<ActionResult<List<IngredientModel>>> GetAllIngredients()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());

            return Ok(result);
        }

        [RolesAuth(Role.Admin)]
        [HttpPost("add-ingredient")]
        public async Task<ActionResult> AddIngredient([FromBody] AddIngredientRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok();
        }
    }
}