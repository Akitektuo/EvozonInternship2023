using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using MealPlan.Business.Menus.Models;
using System;
using MealPlan.Business.Exceptions;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    public class GetMenuQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetMenuQueryHandler _handler;
        private GetMenuQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetMenuQueryHandler(_context.Object);

            CreateRequest();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task WhenIdExists_ShouldReturnCorrectMenu()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Id.Should().Be(1);
            var expectedResult = new GetMenuDetailsModel
            {
                Id = 1,
                Name = "Menu1",
                Category = (int)MenuType.Fitness,
                Price = (float)31.6,
                Meals = new List<GetMenuMealModel>
                {
                    new GetMenuMealModel
                    {
                        Id = 1,
                        Name = "Meal1",
                        Description = "GoodMeal1",
                        MealTypeId = 1,
                        Price = (float)5.3,
                        Ingredients = new List<string>
                        {
                            "Ingredient1",
                            "Ingredient2"
                        }
                    },
                    new GetMenuMealModel
                    {
                        Id = 2,
                        Name = "Meal2",
                        Description = "GoodMeal2",
                        MealTypeId = 2,
                        Price = (float)7.3,
                        Ingredients = new List<string>
                        {
                            "Ingredient3",
                            "Ingredient4"
                        }
                    },
                    new GetMenuMealModel
                    {
                        Id = 3,
                        Name = "Meal3",
                        Description = "GoodMeal3",
                        MealTypeId = 3,
                        Price = (float)12,
                        Ingredients = new List<string>
                        {
                            "Ingredient5",
                            "Ingredient2"
                        }
                    },
                    new GetMenuMealModel
                    {
                        Id = 4,
                        Name = "Meal4",
                        Description = "GoodMeal4",
                        MealTypeId = 4,
                        Price = (float)4,
                        Ingredients = new List<string>
                        {
                            "Ingredient6",
                            "Ingredient7"
                        }
                    },
                    new GetMenuMealModel
                    {
                        Id = 5,
                        Name = "Meal5",
                        Description = "GoodMeal5",
                        MealTypeId = 5,
                        Price = (float)3,
                        Ingredients = new List<string>
                        {
                            "Ingredient8",
                            "Ingredient5"
                        }
                    }
               }
            };

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task WhenInexistentId_ShouldThrowException()
        {
            _request.Id = 11234;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.MenuNotFound);
        }

        private void SetupContext()
        {
            var menuLookup = new List<MenuTypeLookup>
            {
                new MenuTypeLookup { Id = MenuType.Fitness , Name = MenuType.Fitness.ToString()},
                new MenuTypeLookup { Id = MenuType.Gym , Name = MenuType.Gym.ToString()},
                new MenuTypeLookup { Id = MenuType.FoodLover , Name = MenuType.FoodLover.ToString()},
                new MenuTypeLookup { Id = MenuType.Vegetarian , Name = MenuType.Vegetarian.ToString()},
            };

            var mealLookup = new List<MealTypeLookup>
            {
                new MealTypeLookup { Id = MealType.Breakfast , Name = MealType.Breakfast.ToString()},
                new MealTypeLookup { Id = MealType.Lunch , Name = MealType.Lunch.ToString()},
                new MealTypeLookup { Id = MealType.Dinner , Name = MealType.Dinner.ToString()},
                new MealTypeLookup { Id = MealType.Dessert , Name = MealType.Dessert.ToString()},
                new MealTypeLookup { Id = MealType.Snack , Name = MealType.Snack.ToString()},
            };

            var menus = new List<Menu>
            {
                new Menu { Id = 1, Name = "Menu1", MenuTypeId = MenuType.Fitness, MenuType = menuLookup[0] },
                new Menu { Id = 2, Name = "Menu2", MenuTypeId = MenuType.Gym, MenuType = menuLookup[1] },
                new Menu { Id = 3, Name = "Menu3", MenuTypeId = MenuType.FoodLover, MenuType = menuLookup[2] },
                new Menu { Id = 4, Name = "Menu4", MenuTypeId = MenuType.Vegetarian, MenuType = menuLookup[3] },
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient{Id = 1, Name = "Ingredient1"},
                new Ingredient{Id = 2, Name = "Ingredient2"},
                new Ingredient{Id = 3, Name = "Ingredient3"},
                new Ingredient{Id = 4, Name = "Ingredient4"},
                new Ingredient{Id = 5, Name = "Ingredient5"},
                new Ingredient{Id = 6, Name = "Ingredient6"},
                new Ingredient{Id = 7, Name = "Ingredient7"},
                new Ingredient{Id = 8, Name = "Ingredient8"}
            };

            var recipes = new List<Recipe>
            {
                new Recipe {Id = 1, Name = "Recipe1", Description = "Description Recipe1", Ingredients = new List<Ingredient> { ingredients[0], ingredients[1]} },
                new Recipe {Id = 2, Name = "Recipe2", Description = "Description Recipe2", Ingredients = new List<Ingredient> { ingredients[2], ingredients[3]} },
                new Recipe {Id = 3, Name = "Recipe3", Description = "Description Recipe3", Ingredients = new List<Ingredient> { ingredients[4], ingredients[1]} },
                new Recipe {Id = 4, Name = "Recipe4", Description = "Description Recipe4", Ingredients = new List<Ingredient> { ingredients[5], ingredients[6]} },
                new Recipe {Id = 5, Name = "Recipe5", Description = "Description Recipe5", Ingredients = new List<Ingredient> { ingredients[7], ingredients[4]} }
            };

            var meals = new List<Meal>
            {
                new Meal { Id = 1, Name = "Meal1", Description = "GoodMeal1", Price = (float)5.3, MealTypeId = mealLookup[0].Id, MenuId = 1, Menu = menus[0], RecipeId = 1, Recipe = recipes[0]},
                new Meal { Id = 2, Name = "Meal2", Description = "GoodMeal2", Price = (float)7.3, MealTypeId = mealLookup[1].Id, MenuId = 1, Menu = menus[0], RecipeId = 2, Recipe = recipes[1]},
                new Meal { Id = 3, Name = "Meal3", Description = "GoodMeal3", Price = (float)12 , MealTypeId = mealLookup[2].Id, MenuId = 1, Menu = menus[1], RecipeId = 3, Recipe = recipes[2]},
                new Meal { Id = 4, Name = "Meal4", Description = "GoodMeal4", Price = (float)4 , MealTypeId = mealLookup[3].Id, MenuId = 1, Menu = menus[1], RecipeId = 4, Recipe = recipes[3]},
                new Meal { Id = 5, Name = "Meal5", Description = "GoodMeal5", Price = (float)3 , MealTypeId = mealLookup[4].Id, MenuId = 1, Menu = menus[1], RecipeId = 5, Recipe = recipes[4]},
                new Meal { Id = 6, Name = "Meal6", Description = "GoodMeal6", Price = (float)6 , MealTypeId = mealLookup[0].Id, MenuId = 2, Menu = menus[1]}
            };

            var recipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Id = 1, RecipeId = 1, IngredientId = 1},
                new RecipeIngredient { Id = 2, RecipeId = 1, IngredientId = 2},
                new RecipeIngredient { Id = 3, RecipeId = 2, IngredientId = 3},
                new RecipeIngredient { Id = 4, RecipeId = 2, IngredientId = 4},
                new RecipeIngredient { Id = 5, RecipeId = 3, IngredientId = 5},
                new RecipeIngredient { Id = 6, RecipeId = 3, IngredientId = 2},
                new RecipeIngredient { Id = 7, RecipeId = 4, IngredientId = 6},
                new RecipeIngredient { Id = 8, RecipeId = 4, IngredientId = 7},
                new RecipeIngredient { Id = 9, RecipeId = 5, IngredientId = 8},
                new RecipeIngredient { Id = 10, RecipeId = 5, IngredientId = 5},

            };

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.RecipeIngredients).ReturnsDbSet(recipeIngredients);
        }

        private void CreateRequest()
        {
            _request = new GetMenuQuery { Id = 1 };
        }
    }
}
