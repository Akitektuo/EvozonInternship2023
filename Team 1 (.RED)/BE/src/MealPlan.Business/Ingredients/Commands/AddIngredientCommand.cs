using MediatR;

namespace MealPlan.Business.Ingredients.Commands
{
    public class AddIngredientCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}