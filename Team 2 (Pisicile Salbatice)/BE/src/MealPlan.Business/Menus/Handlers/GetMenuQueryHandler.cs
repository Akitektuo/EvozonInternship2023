using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MealPlan.Business.Exceptions;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, GetMenuDetailsModel>
    {
        private readonly MealPlanContext _context;

        public GetMenuQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<GetMenuDetailsModel> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var menuModel = await GetMenu(request.Id);

            if (menuModel == null)
            {
                throw new CustomApplicationException(ErrorCode.MenuNotFound, "The menu does not exist.");
            }

            return menuModel;
        }

        private List<GetMenuMealModel> GetMeals(int menuId)
        {
            return _context.Meals
                .Where(x => x.MenuId == menuId)
                .Select(meal => new GetMenuMealModel
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    MealTypeId = (int)meal.MealTypeId,
                    Price = meal.Price,
                    Description = meal.Description,
                    Ingredients = meal.Recipe.Ingredients.Select(x => x.Name).ToList()
                })
                .ToList();
        }

        private async Task<GetMenuDetailsModel> GetMenu(int menuId)
        {
            return await _context.Menus
                .Where(x => x.Id == menuId)
                .Select(q => new GetMenuDetailsModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Category = (int)q.MenuType.Id,
                    Meals = GetMeals(menuId),
                    Price = _context.Meals.Where(meal => meal.MenuId == menuId).Select(meal => meal.Price).Sum()
                })
                .FirstOrDefaultAsync();
        }
    }
}
