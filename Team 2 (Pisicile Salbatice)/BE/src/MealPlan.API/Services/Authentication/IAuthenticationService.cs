using MealPlan.Business.Users.Models;

namespace MealPlan.API.Services.Authentication
{
    public interface IAuthenticationService
    {
        LoginModel GenerateJwt(LoginModel user);
    }
}