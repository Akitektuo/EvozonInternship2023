using FluentValidation;

namespace MealPlan.API.Requests.Recipes
{
    public class GetRecipeRequest
    {
        public int RecipeId { get; set; }
    }

    public class GetRecipeRequestValidator : AbstractValidator<GetRecipeRequest>
    {
        public GetRecipeRequestValidator()
        {
            RuleFor(x => x.RecipeId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
