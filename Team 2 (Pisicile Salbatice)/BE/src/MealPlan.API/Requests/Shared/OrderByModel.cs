using FluentValidation;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Shared
{
    public class OrderByModel
    {
        public string Column { get; set; }
        public string SortOrder { get; set; } = "asc";
    }

    public class OrderByModelValidator : AbstractValidator<OrderByModel>
    {
        public OrderByModelValidator()
        {
            RuleFor(x => x.SortOrder)
                .Must(y => IsValidSortOrder(y))
                .WithMessage("Sort order must be either 'desc' or 'asc'.");
        }

        public bool IsValidSortOrder(string sortOrder)
        {
            var validEntries = new List<string>
            {
                "asc",
                "desc"
            };

            return validEntries.Contains(sortOrder.ToLower());
        }
    }
}
