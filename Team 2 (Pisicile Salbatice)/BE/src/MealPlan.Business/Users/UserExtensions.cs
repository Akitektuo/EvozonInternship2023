using MealPlan.Business.Users.Models;
using MealPlan.Data.Models.Users;
using System.Linq;

namespace MealPlan.Business.Users
{
    public static class UserExtensions
    {
        public static IQueryable<LoginModel> ToLoginModel(this IQueryable<User> query)
        {
            return query.Select(q => new LoginModel
            {
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                Id = q.Id,
                RoleId = q.RoleId, 
                Balance = q.Balance
            });
        }
    }
}
