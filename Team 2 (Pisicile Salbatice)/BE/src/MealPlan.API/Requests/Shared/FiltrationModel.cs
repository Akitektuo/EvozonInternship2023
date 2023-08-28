using FluentValidation;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Shared
{
    public class FiltrationModel
    {
        public List<ColumnClauseModel> ColumnClauses { get; set; } = new List<ColumnClauseModel>();
        public string SearchText { get; set; } = string.Empty;
        public List<OrderByModel> OrderByModels { get; set; } = new List<OrderByModel>();
    }

    public class FiltrationModelValidator : AbstractValidator<FiltrationModel>
    {
        public FiltrationModelValidator()
        {
            RuleForEach(x => x.OrderByModels)
                .SetValidator(new OrderByModelValidator());
        }
    }
}
