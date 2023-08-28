using MealPlan.Data.Models.Recipes;
using System.Linq;

namespace MealPlan.Business.Recipes
{
    public static class IngredientExtensions
    {
        public static IQueryable<RecipeIngredient> ToRecipeIngredientWithRecipe(this IQueryable<Ingredient> query, Recipe recipe)
        {
            return query.Select(q => new RecipeIngredient
            {
                RecipeId = recipe.Id,
                IngredientId = q.Id
            });
        }
    }
}
