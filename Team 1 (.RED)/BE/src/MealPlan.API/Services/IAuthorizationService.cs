using MealPlan.Data.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MealPlan.API.Services
{
    public interface IAuthorizationService 
    {
        Task<bool> IsAuthorized(string email, List<Role> allowedRoles);
    }
}