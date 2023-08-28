using FluentValidation;

namespace MealPlan.API.Requests.Orders
{
    public class ApproveOrderRequest
    {
        public int OrderID { get; set; }
    }

    public class ApproveOrderRequestValidator : AbstractValidator<ApproveOrderRequest>
    {
        public ApproveOrderRequestValidator()
        {
            RuleFor(x => x.OrderID).NotEmpty().GreaterThan(0);
        }
    }
}