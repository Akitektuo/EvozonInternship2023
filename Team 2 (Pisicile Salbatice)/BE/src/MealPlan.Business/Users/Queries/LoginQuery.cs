using MealPlan.Business.Users.Models;
using MediatR;

namespace MealPlan.Business.Users.Queries
{
    public class LoginQuery: IRequest<LoginModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
