using FluentValidation;
using MealPlan.API.Requests.Utils;
using MealPlan.Data.Models.Orders;

namespace MealPlan.API.Requests.Orders
{
    public class GetAllOrdersRequest : PageRequest
    {
        public FilteringModel FilteringModel { get; set; } = new FilteringModel();
        public OrderingModel OrderingModel { get; set; } = new OrderingModel();
    }

    public class GetAllOrdersRequestValidator : AbstractValidator<GetAllOrdersRequest>
    {
        public GetAllOrdersRequestValidator()
        {
            Include(new PageRequestValidator());

            RuleFor(x => x.FilteringModel).NotEmpty();
            RuleFor(x => x.OrderingModel).NotEmpty();
            RuleFor(x => x.OrderingModel.OrderByColumns).MaximumLength(100);
            RuleFor(x => x.FilteringModel.SearchText).MaximumLength(50);

            When(x => x.FilteringModel.FilterByStatus != 0, () =>
            {
                RuleFor(x => x.FilteringModel.FilterByStatus).IsInEnum();
            });
        }
    }
    public class FilteringModel
    {
        public OrderStatus FilterByStatus { get; set; } = 0;
        public string SearchText { get; set; }
    }

    public class OrderingModel
    {
        public bool OrderByDescending { get; set; } = false;
        public string OrderByColumns { get; set; } = "StartDate";
    }
}