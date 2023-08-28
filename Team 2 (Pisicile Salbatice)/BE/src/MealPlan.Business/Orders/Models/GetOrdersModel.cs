using System.Collections.Generic;

namespace MealPlan.Business.Orders.Models
{
    public class GetOrdersModel
    {
        public List<OrderModel> Orders { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}
