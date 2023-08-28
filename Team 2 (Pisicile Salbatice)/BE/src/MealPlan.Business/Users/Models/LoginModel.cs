using MealPlan.Data.Models.Users;

namespace MealPlan.Business.Users.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role RoleId { get; set; }
        public string Token { get; set; }
        public float Balance { get; set; }
    }
}
