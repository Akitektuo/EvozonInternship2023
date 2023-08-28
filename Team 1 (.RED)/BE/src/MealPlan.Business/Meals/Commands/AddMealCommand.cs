using MealPlan.Data.Models.Meals;
using MediatR;

namespace MealPlan.Business.Meals.Commands
{
    public class AddMealCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MealType MealTypeId { get; set; }
        public int RecipeId { get; set; }
    }
}