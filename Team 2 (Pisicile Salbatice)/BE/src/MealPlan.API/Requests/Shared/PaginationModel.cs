using FluentValidation;

namespace MealPlan.API.Requests.Shared
{
    public class PaginationModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PaginationModelValidator : AbstractValidator<PaginationModel>
    {
        public PaginationModelValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
