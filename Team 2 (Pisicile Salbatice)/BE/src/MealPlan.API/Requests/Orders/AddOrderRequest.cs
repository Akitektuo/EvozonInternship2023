using FluentValidation;
using System;

namespace MealPlan.API.Requests.Orders
{
    public class AddOrderRequest
    {
        public int MenuId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class AddOrderRequestValidator: AbstractValidator<AddOrderRequest>
    {
        public AddOrderRequestValidator()
        {
            RuleFor(x => x.MenuId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today.AddDays(1))
                .LessThan(DateTime.Today.AddDays(30));
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(y => y.StartDate)
                .LessThan(DateTime.Today.AddDays(30));
            RuleFor(x => x)
                .Must(request => (request.EndDate - request.StartDate).Days < 30)
                .WithMessage("The difference between StartDate and EndDate must be less than 30 days.");
        }
    }
}
