using MealPlan.Business.Recipes.Models;
using MediatR;

namespace MealPlan.Business.Recipes.Queries
{
    public class GetRecipeQuery : IRequest<RecipeDetails>
    {
        public int Id { get; set; }
    }
}