using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Business.Utils;
using MealPlan.Data;
using MealPlan.Data.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Menus.Handlers
{
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, PaginationModel<MenuOverview>>
    {

        private readonly MealPlanContext _context;

        public GetAllMenusQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<PaginationModel<MenuOverview>> Handle(GetAllMenusQuery query, CancellationToken cancellationToken)
        {
            var totalElements = await _context.Menus.Where(x => x.CategoryId.Equals(query.CategoryId)).CountAsync();

            var menuList = await _context.Meals
                .GroupBy(meal => new { MenuId = meal.MenuId, Description = meal.Menu.Description, MenuName = meal.Menu.Name, Category = meal.Menu.CategoryId})
                .Where(x => x.Key.Category.Equals(query.CategoryId))
                .Select(mealGroup => new MenuOverview
                { 
                    Id = mealGroup.Key.MenuId,
                    Name = mealGroup.Key.MenuName, 
                    Description = mealGroup.Key.Description, 
                    Price = mealGroup.Sum(m => m.Price) 
                })
                .ToPage(query.PageNumber, query.PageSize)
                .ToListAsync();

            return new PaginationModel<MenuOverview>
            {
                TotalRecords = totalElements,
                Items = menuList
            };
        }
    }
}