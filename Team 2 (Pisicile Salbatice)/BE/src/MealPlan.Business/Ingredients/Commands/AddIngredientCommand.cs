using MediatR;

namespace MealPlan.Business.Ingredients.Commands
{
    public class AddIngredientCommand : IRequest<bool>
    {
        public string IngredientName { get; set; }
    }
}
