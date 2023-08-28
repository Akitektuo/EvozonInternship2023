using MealPlan.Business.Recipes.Models;
using MealPlan.Data.Models.Recipes;
using System.Linq;

namespace MealPlan.Business.Recipes
{
    public static class RecipeExtensions
    {
        public static IQueryable<RecipeDetails> ToRecipeDetails(this IQueryable<Recipe> query)
        {
            return query.Select(q => new RecipeDetails
            {
                Name = q.Name,
                Description = q.Description,
                Ingredients = q.Ingredients.Select(ingredient => ingredient.Name).ToList()
            });
        }
        
        public static IQueryable<RecipeOverview> ToRecipeOverview(this IQueryable<Recipe> query)
        {
            return query.Select(q => new RecipeOverview
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description
            });
        }
    }
}