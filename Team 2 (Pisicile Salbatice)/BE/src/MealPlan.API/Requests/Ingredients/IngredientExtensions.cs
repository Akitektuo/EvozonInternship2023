using MealPlan.Business.Ingredients.Commands;

namespace MealPlan.API.Requests.Ingredients
{
    public static class IngredientExtensions
    {
        public static AddIngredientCommand ToCommand(this AddIngredientRequest request)
        {
            return new AddIngredientCommand
            {
                IngredientName = request.IngredientName.Trim()
            };
        }
    }
}
