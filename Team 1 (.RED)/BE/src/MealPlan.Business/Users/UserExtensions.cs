using MealPlan.Business.Users.Models;
using MealPlan.Data.Models.Users;
using System.Linq;

namespace MealPlan.Business.Users
{
    public static class UserExtensions
    {
        public static IQueryable<UserDetails> ToUserDetails(this IQueryable<User> query)
        {
            return query.Select(q => new UserDetails
            {
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                Role = q.RoleId.ToString(),
            });
        }
    }
}