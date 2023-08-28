using MealPlan.Business.Users.Queries;
using MealPlan.Business.Users.Commands;

namespace MealPlan.API.Requests.Users
{
    public static class UserExtensions
    {
        public static LoginQuery ToQuery(this LoginRequest request)
        {
            return new LoginQuery
            {
                Email = request.Email,
                Password = request.Password
            };
        }
        
        public static RegisterCommand ToCommand(this RegisterRequest request)
        {
            return new RegisterCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmedPassword = request.ConfirmedPassword
            };
        }

        public static GetUserRoleQuery ToQuery(this GetUserRoleRequest request)
        {
            return new GetUserRoleQuery
            {
                Email = request.Email
            };
        }
    }
}
