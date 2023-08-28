using MealPlan.Business.Exceptions;
using MealPlan.Business.Users.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Users.Handlers
{
    public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, double>
    {
        private readonly MealPlanContext _context;

        public GetWalletQueryHandler(MealPlanContext context) 
        { 
            _context = context;
        }

        public async Task<double> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .Where(x => x.Email == request.Email)
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomApplicationException(ErrorCode.InvalidCredentials, "Invalid credentials");
            }

            return result.WalletBalance;
        }
    }
}