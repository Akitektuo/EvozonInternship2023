using MealPlan.Business.Menus.Queries;
using MealPlan.Business.Menus.Commands;
using MealPlan.API.Requests.Meals;
using System.Linq;
using MealPlan.Business.Menus.Commands.AddGeneratedMenu;
using MealPlan.API.Requests.Menus.AddGeneratedMenu;
using System.Collections.Generic;

namespace MealPlan.API.Requests.Menus
{
    public static class MenuExtensions
    {
        public static GetAllMenusQuery ToQuery(this GetAllMenusRequest request)
        {
            return new GetAllMenusQuery
            {
                Category = request.Category,
                PageNumber = request.PaginationModel.PageNumber,
                PageSize = request.PaginationModel.PageSize
            };
        }

        public static AddMenuCommand ToCommand(this AddMenuRequest request)
        {
            return new AddMenuCommand
            {
                MenuName = request.MenuName,
                MenuTypeId = request.MenuTypeId,
                Meals = request.Meals.ToMealModels().ToList()
            };
        }

        public static GetGeneratedMenuQuery ToQuery(this GetGeneratedMenuRequest request)
        {
            return new GetGeneratedMenuQuery
            {
                MenuType = request.MenuType,
                PriceSuggestion = request.PriceSuggestion
            };
        }
        
        public static GetMenuQuery ToQuery(this GetMenuRequest request)
        {
            return new GetMenuQuery
            {
                Id = request.MenuId
            };
        }

        public static AddGeneratedMenuCommand ToCommand(this AddGeneratedMenuRequest request)
        {
            return new AddGeneratedMenuCommand
            {
                Name = request.Name,
                MenuTypeId = request.MenuTypeId,
                Meals = request.Meals.ToGeneratedMealModel(),

            };
        }

        static List<GeneratedMealModel> ToGeneratedMealModel(this List<GeneratedMeal> meal)
        {
            return meal
                .Select(x => new GeneratedMealModel
                {
                    Description = x.Description,
                    MealTypeId = x.MealTypeId,
                    Name = x.Name,
                    Price = x.Price,
                    Recipe = x.Recipe.ToGeneratedRecipeModel()
                })
                .ToList();
        }

        static GeneratedRecipeModel ToGeneratedRecipeModel(this GeneratedRecipe recipe)
        {
            return new GeneratedRecipeModel
            {
                Description = recipe.Description,
                Name = recipe.Name,
                Ingredients = recipe.Ingredients
            };
        }
    }
}
