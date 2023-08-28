using FluentValidation;

namespace MealPlan.API.Requests.Menus
{
    public class GetMenuRequest
    {
        public int MenuId { get; set; }
    }

    public class GetMenuRequestValidator : AbstractValidator<GetMenuRequest>
    {
        public GetMenuRequestValidator()
        {
            RuleFor(x => x.MenuId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
