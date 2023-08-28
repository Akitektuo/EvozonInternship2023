using MealPlan.Business.Users.Commands;
using MealPlan.Business.Users.Queries;
using System.Text.RegularExpressions;

namespace MealPlan.API.Requests.Users
{
    public static class UserExtensions
    {
        public static RegisterUserCommand ToCommand(this RegisterUserRequest request)
        {
            return new RegisterUserCommand
            {
                FirstName = Regex.Replace(request.FirstName.Trim(), "\\s+", " "),
                LastName = Regex.Replace(request.LastName.Trim(), "\\s+", " "),
                Email = request.Email.Trim(),
                Password = request.Password
            };
        }
        
        public static LoginQuery ToQuery(this LoginRequest request)
        {
            return new LoginQuery
            {
                Email = request.Email.Trim(),
                Password = request.Password.TrimEnd()
            };
        }
    }
}