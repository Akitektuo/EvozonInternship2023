using MealPlan.Business.Ingredients.Models;
using MealPlan.Data.Models.Recipes;
using System.Linq;

namespace MealPlan.Business.Ingredients
{
    public static class IngredientExtensions
    {
        public static IQueryable<IngredientModel> ToIngredientModel(this IQueryable<Ingredient> ingredients)
        {
            return ingredients.Select(r => new IngredientModel
            {
                Id = r.Id,
                Name = r.Name
            });
        }
    }
}
