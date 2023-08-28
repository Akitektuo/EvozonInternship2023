using FluentValidation;
using MealPlan.API.Utils;
using System;

namespace MealPlan.API.Requests.Orders
{
    public class AddOrderRequest
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MenuId { get; set; }
    }

    public class AddOrderRequestValidator : AbstractValidator<AddOrderRequest>
    {
        public AddOrderRequestValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(70);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .Matches(RegexConstants.PhoneNumber);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThan(DateTime.Today)
                .LessThanOrEqualTo(DateTime.Today.AddDays(31));

            RuleFor(x => x.EndDate)
                .NotEmpty()              
                .GreaterThan(DateTime.Today)
                .LessThanOrEqualTo(DateTime.Today.AddDays(31));

            RuleFor(x => new { x.StartDate, x.EndDate })
                .Must(x => x.StartDate <= x.EndDate)
                .WithName("Dates")
                .WithMessage("'StartDate' must be smaller or equal to 'EndDate'.");

            RuleFor(x => x.MenuId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}