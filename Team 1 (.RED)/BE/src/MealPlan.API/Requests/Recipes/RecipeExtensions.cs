using MealPlan.Business.Recipes.Commands;
using MealPlan.Business.Recipes.Queries;

namespace MealPlan.API.Requests.Recipes
{
    public static class RecipeExtensions
    {
        public static GetRecipeQuery ToQuery(this GetRecipeRequest request)
        {
            return new GetRecipeQuery
            {
                Id = request.Id
            };
        }

        public static GetUnusedRecipesQuery ToQuery(this GetUnusedRecipesRequest request)
        {
            return new GetUnusedRecipesQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        
        public static AddRecipeCommand ToCommand(this AddRecipeRequest request) 
        {
            return new AddRecipeCommand
            {
                Description = request.Description,
                Name = request.Name,
                IngredientIds = request.IngredientIds
            };
        } 

        public static AddSuggestedRecipeCommand ToCommand(this AddSuggestedRecipeRequest request)
        {
            return new AddSuggestedRecipeCommand
            {
                Description = request.Description,
                Name = request.Name,
                Ingredients = request.Ingredients
            };
        }
    }
}