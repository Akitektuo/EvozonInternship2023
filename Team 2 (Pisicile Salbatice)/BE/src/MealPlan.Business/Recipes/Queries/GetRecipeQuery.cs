using MealPlan.Business.Recipes.Models;
using MediatR;

namespace MealPlan.Business.Recipes.Queries
{
    public class GetRecipeQuery : IRequest<RecipeModel>
    {
        public int RecipeId { get; set; }
    }
}
