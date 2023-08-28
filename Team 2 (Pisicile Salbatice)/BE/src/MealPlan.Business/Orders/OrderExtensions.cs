using MealPlan.Business.Orders.Models;
using MealPlan.Data.Models.Orders;
using System.Linq;

namespace MealPlan.Business.Orders
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderModel> ToOrderModel(this IQueryable<Order> orders)
        {
            return orders.Select(x => new OrderModel
            {
                Id = x.Id,
                MenuName = x.MenuName,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                StatusId = x.StatusId,
                UserEmail = x.UserEmail
            });
        }
    }
}
