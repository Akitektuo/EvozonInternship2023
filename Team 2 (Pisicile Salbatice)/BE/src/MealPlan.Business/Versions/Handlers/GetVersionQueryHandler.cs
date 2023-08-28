using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MealPlan.Business.Versions.Models;
using MealPlan.Business.Versions.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Business.Versions.Handlers
{
    public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionCode>
    {
        private readonly MealPlanContext _context;

        public GetVersionQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<VersionCode> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            return await _context.ApplicationVersions
                .Where(v => v.Name == request.Name)
                .ToVersionCode()
                .FirstOrDefaultAsync();
        }
    }
}
