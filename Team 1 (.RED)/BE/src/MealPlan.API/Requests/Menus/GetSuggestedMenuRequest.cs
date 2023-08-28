using FluentValidation;
using MealPlan.Data.Models.Menus;

namespace MealPlan.API.Requests.Menus
{
    public class GetSuggestedMenuRequest
    {
        public MenuCategory CategoryId { get; set; }
        public double PriceSuggestion { get; set; }
    }

    public class GetSuggestedMenuRequestValidator : AbstractValidator<GetSuggestedMenuRequest>
    {
        public GetSuggestedMenuRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().IsInEnum();

            RuleFor(x => x.PriceSuggestion).NotEmpty().GreaterThan(10).LessThanOrEqualTo(1000);
        }
    }
}