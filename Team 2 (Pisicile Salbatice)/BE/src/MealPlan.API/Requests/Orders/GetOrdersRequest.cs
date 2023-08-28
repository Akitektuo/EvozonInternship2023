using FluentValidation;
using MealPlan.API.Requests.Shared;
using FiltrationModel = MealPlan.API.Requests.Shared.FiltrationModel;

namespace MealPlan.API.Requests.Orders
{
    public class GetOrdersRequest
    {
        public FiltrationModel Filtration { get; set; } = new FiltrationModel();
        public PaginationModel Pagination { get; set; } = new PaginationModel();
    }

    public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrdersRequestValidator() {
            RuleFor(x => x.Filtration)
                .SetValidator(new FiltrationModelValidator());
            RuleFor(x => x.Pagination)
                .SetValidator(new PaginationModelValidator());
        }
    }
}
