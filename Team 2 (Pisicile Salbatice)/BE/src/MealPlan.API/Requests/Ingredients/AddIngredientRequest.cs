using FluentValidation;

namespace MealPlan.API.Requests.Ingredients
{
    public class AddIngredientRequest
    {
        public string IngredientName { get; set; }
    }

    public class AddIngredientRequestValidator : AbstractValidator<AddIngredientRequest>
    {
        public AddIngredientRequestValidator() { 
            RuleFor(x => x.IngredientName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
