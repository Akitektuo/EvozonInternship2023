using MealPlan.Data.Models.Users;
using MediatR;

namespace MealPlan.Business.Users.Queries
{
    public class GetRoleQuery : IRequest<Role>
    {
        public string Email { get; set; }
    }
}