using MealPlan.Business.Recipes.Commands;
using MealPlan.Business.Recipes.Queries;

namespace MealPlan.API.Requests.Recipes
{
    public static class RecipeExtensions
    {
        public static AddRecipeCommand ToCommand(this AddRecipeRequest request)
        {
            return new AddRecipeCommand
            {
                Name = request.Name,
                Description = request.Description,
                IngredientIds = request.IngredientIds
            };
        }
        
        public static GetRecipeQuery ToQuery(this GetRecipeRequest request)
        {
            return new GetRecipeQuery
            {
                RecipeId = request.RecipeId
            };
        }
        
        public static GetRecipesQuery ToQuery(this GetRecipesRequest request)
        {
            return new GetRecipesQuery
            {
                PageNumber = request.PaginationModel.PageNumber,
                PageSize = request.PaginationModel.PageSize,
                FilterUnusedRecipes = request.FilterUnusedRecipes
            };
        }
    }
}
