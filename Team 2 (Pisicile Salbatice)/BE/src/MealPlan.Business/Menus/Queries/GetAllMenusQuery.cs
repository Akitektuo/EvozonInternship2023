using MealPlan.Business.Menus.Models;
using MealPlan.Data.Models.Meals;
using MediatR;

namespace MealPlan.Business.Menus.Queries
{
    public class GetAllMenusQuery: IRequest<GetAllMenusModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public MenuType Category { get; set; }
    }
}
