using FluentAssertions;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class GetMenuQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetMenuQueryHandler _handler;
        private GetMenuQuery _successfulRequest;
        private GetMenuQuery _failedRequest;
        private Menu _menu;

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
        public async Task WhenMenuIdValid_ShouldReturnCorrectMenu()
        {
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Name.Should().Be(_menu.Name);
            result.Description.Should().Be(_menu.Description);
            result.Price.Should().Be((int)_menu.Meals.Sum(m => m.Price));
        }

        [Test]
        public async Task WhenMenuIdValid_ShouldReturnCorrectMeals()
        {
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.MealsDetails[0].Name.Should().Be(_menu.Meals[0].Name);
            result.MealsDetails[0].MealTypeId.Should().Be(_menu.Meals[0].MealTypeId);
        }

        [Test]
        public async Task WhenMenuIdValid_ShouldReturnCorrectRecipes()
        {
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.MealsDetails[0].RecipeDetails.Description.Should().Be(_menu.Meals[0].Recipe.Description);
            result.MealsDetails[0].RecipeDetails.Ingredients[0].Should().Be(_menu.Meals[0].Recipe.Ingredients[0].Name);
        }

        [Test]
        public async Task WhenMenuIdNotValid_ShouldReturnError()
        {
            Func<Task> action = async () => await _handler.Handle(_failedRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Code = (int)ErrorCode.MenuNotFound,
                        Message = "Menu not found"
                    }));
        }

        private void CreateRequest()
        {
            _successfulRequest = new GetMenuQuery { Id = 1 };
            _failedRequest = new GetMenuQuery { Id= 2 };
        }

        private void SetupContext()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Ing Name 1" },
                new Ingredient { Id = 2, Name = "Ing Name 2" },
                new Ingredient { Id = 3, Name = "Ing Name 3" }
            };

            var menus = new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    Name = "menu1",
                    Description = "description1",
                    Category = new MenuCategoryLookup
                    {
                        Id = MenuCategory.Vegetarian,
                        Name = "Vegetarian"
                    },
                    CategoryId = MenuCategory.Vegetarian,
                    Meals = new List<Meal> 
                    {
                        new Meal
                        {
                            Id = 1,
                            Name = "meal1",
                            Price = 1,
                            MenuId = 1,
                            RecipeId = 1,
                            Recipe =  new Recipe
                            {
                                Id = 1,
                                Description = "Description 1",
                                Name = "Recipe 2",
                                Ingredients = new List<Ingredient>
                                {
                                    ingredients[0]
                                }
                            },
                        },
                        new Meal
                        {
                            Id = 2,
                            Recipe = new Recipe
                            {
                                Id = 2,
                                Ingredients = new List<Ingredient>
                                {
                                    ingredients[1]
                                }
                            }
                        },
                        new Meal
                        {
                            Id = 3,
                            Recipe = new Recipe
                            {
                                Id = 3,
                                Ingredients = new List<Ingredient>
                                {
                                    ingredients[2]
                                }
                            }
                        },
                        new Meal
                        {
                            Id = 4,
                            Recipe = new Recipe
                            {
                                Id = 4,
                                Ingredients = new List<Ingredient>
                                {
                                    ingredients[1]
                                }
                            }
                        },
                        new Meal
                        {
                            Id = 5,
                            Recipe = new Recipe
                            {
                                Id = 5,
                                Ingredients = new List<Ingredient>
                                {
                                    ingredients[0]
                                }
                            }
                        }
                    }
                }
            };

            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _menu = menus[0];
        }
    }
}