using System;

namespace MealPlan.Business.Orders.Models
{
    public class OrderDetails
    {
        public string UserEmail { get; set; }
        public string MenuName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderStatus { get; set; }
    }
}