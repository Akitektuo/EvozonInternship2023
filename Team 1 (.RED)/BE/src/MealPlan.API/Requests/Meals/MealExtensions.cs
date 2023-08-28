using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Meals.Commands;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Meals
{
    public static class MealExtensions
    {
        public static AddMealCommand ToCommand(this AddMealRequest request)
        {
            return new AddMealCommand
            {
                Name = request.Name,
                Price = request.Price,
                MealTypeId = (MealType)request.MealTypeId,
                RecipeId = request.RecipeId
            };
        }

        public static AddSuggestedMealCommand ToCommand(this AddSuggestedMealRequest request)
        {
            return new AddSuggestedMealCommand
            {
                Name = request.Name,
                Price = request.Price,
                MealTypeId = (MealType)request.MealTypeId,
                Recipe = request.Recipe.ToCommand()
            };
        }
    }
}