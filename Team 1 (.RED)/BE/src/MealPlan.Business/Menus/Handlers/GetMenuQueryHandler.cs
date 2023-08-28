using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, MenuDetails>
    {
        private readonly MealPlanContext _context;

        public GetMenuQueryHandler(MealPlanContext context) 
        {
            _context = context;
        }

        public async Task<MenuDetails> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Menus
                .Include(menu => menu.Meals)
                .ThenInclude(meal => meal.Recipe)
                .ThenInclude(recipe => recipe.Ingredients)
                .Where(x => x.Id == request.Id)
                .ToMenuDetails()
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomApplicationException(ErrorCode.MenuNotFound, "Menu not found");
            }

            return result;
        }
    }
}