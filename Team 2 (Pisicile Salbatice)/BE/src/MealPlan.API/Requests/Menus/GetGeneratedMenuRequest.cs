using FluentValidation;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Menus
{
    public class GetGeneratedMenuRequest
    {
        public MenuType MenuType { get; set; }
        public float PriceSuggestion { get; set; }
    }

    public class GetGeneratedMenuRequestValidator : AbstractValidator<GetGeneratedMenuRequest>
    {
        public GetGeneratedMenuRequestValidator()
        {
            RuleFor(x => x.MenuType)
                .NotEmpty()
                .IsInEnum();
            RuleFor(x => x.PriceSuggestion)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
