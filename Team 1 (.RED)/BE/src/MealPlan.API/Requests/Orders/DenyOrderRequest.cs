using FluentValidation;

namespace MealPlan.API.Requests.Orders
{
    public class DenyOrderRequest
    {
        public int OrderID { get; set; }
    }

    public class DenyOrderRequestValidator : AbstractValidator<DenyOrderRequest>
    {
        public DenyOrderRequestValidator()
        {
            RuleFor(x => x.OrderID).NotEmpty().GreaterThan(0);
        }
    }
}