using MealPlan.API.Requests.Users;
using MealPlan.Data.Models.Users;
using System.Threading.Tasks;

namespace MealPlan.API.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(GetUserRoleRequest request);
    }
}