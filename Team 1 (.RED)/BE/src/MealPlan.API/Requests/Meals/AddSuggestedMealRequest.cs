using FluentValidation;
using MealPlan.API.Requests.Recipes;

namespace MealPlan.API.Requests.Meals
{
    public class AddSuggestedMealRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int MealTypeId { get; set; }

        public AddSuggestedRecipeRequest Recipe { get; set; }
    }

    public class AddSuggestedMealRequestValidator : AbstractValidator<AddSuggestedMealRequest>
    {
        public AddSuggestedMealRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.MealTypeId)
                .NotNull()
                .GreaterThan(0)
                .IsInEnum();

            RuleFor(x => x.Recipe)
                .NotNull();
        }
    }
}