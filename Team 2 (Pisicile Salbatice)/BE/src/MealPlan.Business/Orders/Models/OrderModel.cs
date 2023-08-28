using MealPlan.Data.Models.Orders;
using System;

namespace MealPlan.Business.Orders.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string UserEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status StatusId { get; set; }
    }
}
