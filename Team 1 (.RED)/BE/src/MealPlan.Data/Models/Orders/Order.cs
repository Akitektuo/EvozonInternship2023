using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Users;
using System;

namespace MealPlan.Data.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public OrderStatus OrderStatusId { get; set; }
        public int MenuId { get; set; }

        public OrderStatusLookup OrderStatus { get; set; }
        public User User { get; set; }
        public Menu Menu { get; set; }
    }
}