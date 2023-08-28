using MealPlan.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MediatR;
using MealPlan.Data.Models.Meals;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, GetAllMenusModel>
    {
        private readonly MealPlanContext _context;

        public GetAllMenusQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<GetAllMenusModel> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            int menusCount = await GetAllMenusCount(request.Category);

            var menuModels = await GetAllMenus(request);

            return new GetAllMenusModel
            {
                MenusList = menuModels,
                TotalMenusCount = menusCount
            };
        }

        private async Task<List<MenuModel>> GetAllMenus(GetAllMenusQuery request)
        {
            return await _context.Menus
                .Where(m => m.MenuTypeId == request.Category)
                .Select(menu => new MenuModel
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Price = _context.Meals.Where(meal => meal.MenuId == menu.Id).Select(meal => meal.Price).Sum()
                })
                .OrderBy(m => m.Name)
                .Pagination(request.PageNumber, request.PageSize)
                .ToListAsync();
        }

        private async Task<int> GetAllMenusCount(MenuType category)
        {
            return await _context.Menus.CountAsync(menu => menu.MenuTypeId == category);
        }
    }
}
