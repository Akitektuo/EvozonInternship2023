using FluentValidation;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Meals
{
    public class AddMealRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public MealType MealTypeId { get; set; }
        public int RecipeId { get; set; }
    }

    public class AddMealRequestValidator : AbstractValidator<AddMealRequest>
    {
        public AddMealRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.MealTypeId).NotNull().IsInEnum();
            RuleFor(x => x.RecipeId).NotNull().GreaterThan(0);
        }
    }
}