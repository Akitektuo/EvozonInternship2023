using FluentValidation;
using MealPlan.Data.Models.Orders;

namespace MealPlan.API.Requests.Orders
{
    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public Status StatusId { get; set; }
    }

    public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
    {
        public UpdateOrderStatusRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.StatusId)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
