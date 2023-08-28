using MealPlan.Data.Models.Orders;
using System.Collections.Generic;

namespace MealPlan.Data.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role RoleId { get; set; }
        public double WalletBalance { get; set; }

        public RoleLookup Role { get; set; }
        public List<Order> Orders { get; set; }
    }
}