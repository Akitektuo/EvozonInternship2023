using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class GetUserRoleHandler : IRequestHandler<GetUserRoleQuery, Role>
    {
        private readonly MealPlanContext _context;

        public GetUserRoleHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<Role> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Email == request.Email)
                .Select(u => u.RoleId)
                .SingleOrDefaultAsync();
        }
    }
}
