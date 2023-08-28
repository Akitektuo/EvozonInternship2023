using FluentValidation;

namespace MealPlan.API.Requests.Utils
{
    public abstract class PageRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PageRequestValidator : AbstractValidator<PageRequest>
    {
        public PageRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}