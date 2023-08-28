using FluentValidation;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Recipes
{
    public class AddRecipeRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<int> IngredientIds { get; set; }
    }

    public class AddRecipeRequestValidator : AbstractValidator<AddRecipeRequest>
    {
        public AddRecipeRequestValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(250);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.IngredientIds).NotEmpty();
        }
    }
}