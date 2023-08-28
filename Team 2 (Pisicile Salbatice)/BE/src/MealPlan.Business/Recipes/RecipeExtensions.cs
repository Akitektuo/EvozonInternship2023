using MealPlan.Business.Recipes.Models;
using MealPlan.Data.Models.Recipes;
using System.Linq;

namespace MealPlan.Business.Recipes
{
    public static class RecipeExtensions
    {
        public static IQueryable<RecipeModel> ToRecipeModel(this IQueryable<Recipe> query)
        {
            return query.Select(q => new RecipeModel
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                Ingredients = q.Ingredients.Select(x => x.Name).ToList()
            }); 
        }
        
        public static IQueryable<RecipeResponse> ToRecipeResponse(this IQueryable<Recipe> recipes)
        {
            return recipes.Select(r => new RecipeResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            });
        }
    }
}
