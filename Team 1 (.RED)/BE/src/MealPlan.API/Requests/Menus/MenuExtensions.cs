using MealPlan.API.Requests.Meals;
using MealPlan.Business.Menus.Commands;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data.Models.Menus;
using System.Linq;

namespace MealPlan.API.Requests.Menus
{
    public static class MenuExtensions
    {
        public static GetAllMenusQuery ToQuery(this GetAllMenusRequest request)
        {
            return new GetAllMenusQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                CategoryId = request.CategoryId
            };
        }

        public static AddMenuCommand ToCommand(this AddMenuRequest request)
        {
            return new AddMenuCommand
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Meals = request.Meals.Select(m => m.ToCommand()).ToList()
            };
        }

        public static GetSuggestedMenuQuery ToQuery(this GetSuggestedMenuRequest request)
        {
            return new GetSuggestedMenuQuery
            {
                CategoryId = request.CategoryId,
                PriceSuggestion = request.PriceSuggestion
            };
        }

        public static AddSuggestedMenuCommand ToCommand(this AddSuggestedMenuRequest request)
        {
            return new AddSuggestedMenuCommand
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Meals = request.Meals.Select(m => m.ToCommand()).ToList()
            };
        }
        
        public static GetMenuQuery ToQuery(this GetMenuRequest request)
        {
            return new GetMenuQuery
            {
                Id = request.Id
            };
        }
    }
}