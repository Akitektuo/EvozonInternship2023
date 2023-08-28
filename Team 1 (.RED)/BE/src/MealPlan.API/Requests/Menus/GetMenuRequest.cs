using FluentValidation;

namespace MealPlan.API.Requests.Menus
{
    public class GetMenuRequest
    {
        public int Id { get; set; }
    }

    public class GetMenuRequestValidator : AbstractValidator<GetMenuRequest>
    {
        public GetMenuRequestValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}