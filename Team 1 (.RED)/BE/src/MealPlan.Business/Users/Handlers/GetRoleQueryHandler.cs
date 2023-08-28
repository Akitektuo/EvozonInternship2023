using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Role>
    {
        private readonly MealPlanContext _context;

        public GetRoleQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<Role> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .Where(x => x.Email == request.Email)
                .Select(x => x.RoleId)
                .SingleOrDefaultAsync();

            return result;
        }
    }
}