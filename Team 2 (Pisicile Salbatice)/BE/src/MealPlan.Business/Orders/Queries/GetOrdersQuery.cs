using MealPlan.Business.Orders.Models;
using MealPlan.Business.Shared;
using MediatR;

namespace MealPlan.Business.Orders.Queries
{
    public class GetOrdersQuery : IRequest<GetOrdersModel>
    {
        public FiltrationModel FiltrationModel { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
