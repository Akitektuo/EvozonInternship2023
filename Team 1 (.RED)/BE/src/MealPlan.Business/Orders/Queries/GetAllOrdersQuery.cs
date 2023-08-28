using MealPlan.Business.Orders.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Orders;
using MediatR;

namespace MealPlan.Business.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<PaginationModel<OrderDetails>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public OrderStatus FilterByStatus { get; set; }
        public string SearchText { get; set; }
        public bool OrderByDescending { get; set; }
        public string OrderByColumns { get; set; }
    }
}