using FluentValidation;
using MealPlan.API.Requests.Meals;

namespace MealPlan.API.Requests.Ingredients
{
    public class AddIngredientRequest
    {
        public string Name { get; set; }
    }
    public class AddIngredientRequestValidator : AbstractValidator<AddIngredientRequest>
    {
        public AddIngredientRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}