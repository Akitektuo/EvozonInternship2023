using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Users;
using System;

namespace MealPlan.Data.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string MenuName { get; set; }
        public string UserEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public Status StatusId { get; set; }

        public Menu Menu { get; set; }
        public User User { get; set; }
        public StatusLookup Status { get; set; }
    }
}
