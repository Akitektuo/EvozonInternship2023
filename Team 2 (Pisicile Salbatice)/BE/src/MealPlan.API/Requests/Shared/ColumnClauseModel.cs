using FluentValidation;

namespace MealPlan.API.Requests.Shared
{
    public class ColumnClauseModel
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }

    public class ColumnClauseModelValidator : AbstractValidator<ColumnClauseModel>
    {
        public ColumnClauseModelValidator() { }
    }
}