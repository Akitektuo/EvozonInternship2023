using FluentValidation;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Recipes
{
    public class AddRecipeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> IngredientIds { get; set; }
    }

    public class AddRecipeRequestValidator : AbstractValidator<AddRecipeRequest>
    {
        public AddRecipeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(x => x.IngredientIds)
                .NotEmpty();
            RuleForEach(x => x.IngredientIds)
                .GreaterThan(0);
        }
    }
}
