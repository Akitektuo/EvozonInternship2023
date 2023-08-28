using FluentValidation;

namespace MealPlan.API.Requests.Recipes
{
    public class GetRecipeRequest
    {
        public int Id { get; set; }
    }

    public class GetRecipeRequestValidator : AbstractValidator<GetRecipeRequest>
    {
        public GetRecipeRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}