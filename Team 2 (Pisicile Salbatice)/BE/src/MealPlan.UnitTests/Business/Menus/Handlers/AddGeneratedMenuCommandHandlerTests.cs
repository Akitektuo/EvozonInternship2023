using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Commands.AddGeneratedMenu;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Recipes;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class AddGeneratedMenuCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddGeneratedMenuCommandHandler _handler;
        private AddGeneratedMenuCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddGeneratedMenuCommandHandler(_context.Object);

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
        public async Task WhenRequestIsValid_ShouldNotThrowExceptions()
        {
            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().NotThrowAsync();
        }

        [Test]
        public async Task WhenUsingDuplicateMenuName_ShouldThrowExceptions()
        {
            _request.Name = "M1";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.MenuNameDuplicated);
        }

        [Test]
        public async Task WhenUsingDuplicateMealName_ShouldThrowExceptions()
        {
            _request.Meals[0].Name = "M1";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.MealNameDuplicated);
        }

        [Test]
        public async Task WhenUsingDuplicateRecipeName_ShouldThrowExceptions()
        {
            _request.Meals[0].Recipe.Name = "testalready";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.RecipeAlredyUsed);
        }

        private void CreateRequest()
        {
            _request = new AddGeneratedMenuCommand
            {
                Name = "M2",
                MenuTypeId = MenuType.Fitness,
                Meals = new List<GeneratedMealModel>()
                {
                    new GeneratedMealModel{
                        Name = "M10",
                        Description = "d1",
                        Price = 20,
                        MealTypeId = MealType.Snack,
                        Recipe = new GeneratedRecipeModel
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMealModel{
                        Name = "M12",
                        Description = "d2",
                        Price = 20,
                        MealTypeId = MealType.Dessert,
                        Recipe = new GeneratedRecipeModel
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMealModel{
                        Name = "M13",
                        Description = "d3",
                        Price = 20,
                        MealTypeId = MealType.Breakfast,
                        Recipe = new GeneratedRecipeModel
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMealModel{
                        Name = "M14",
                        Description = "d4",
                        Price = 20,
                        MealTypeId = MealType.Lunch,
                        Recipe = new GeneratedRecipeModel
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMealModel{
                        Name = "M15",
                        Description = "d5",
                        Price = 20,
                        MealTypeId = MealType.Dinner,
                        Recipe = new GeneratedRecipeModel
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                }
            };
        }
        private void SetupContext()
        {
            var recipes = new List<Recipe>()
            {
                new Recipe
                {
                    Id = 1,
                    Name = "r1",
                    Description = "desc1"
                },
                new Recipe{
                    Id = 2,
                    Name = "r2",
                    Description = "desc2"
                },
                new Recipe
                {
                    Id = 3,
                    Name = "r3",
                    Description = "desc3"
                },
                new Recipe
                {
                    Id = 4,
                    Name = "r4",
                    Description = "desc4"
                },
                new Recipe
                {
                    Id = 5,
                    Name = "r5",
                    Description = "desc5"
                },
                new Recipe
                {
                    Id = 6,
                    Name = "r6",
                    Description = "desc6"
                },
                new Recipe
                {
                    Id = 7,
                    Name = "testalready",
                    Description = "test"
                }
            };

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    RoleId = Role.User,
                    Email = "user1@email.com",
                    FirstName = "f1",
                    LastName = "l1"
                },
                new User
                {
                    Id = 2,
                    RoleId = Role.Admin,
                    Email = "user2@email.com",
                    Password = "123454656",
                    FirstName = "f2",
                    LastName = "l2"
                }
            };

            var menu = new List<Menu>()
            {
                new Menu
                {
                Id = 1,
                Name = "M1",
                MenuTypeId = MenuType.Fitness
                }
            };

            var meals = new List<Meal>()
            {
                new Meal
                {
                    Id = 1,
                    Name = "M1",
                    Description = "d1",
                    Price = 20,
                    MealTypeId = MealType.Snack,
                    RecipeId = 6
                }
            };

            var ingredients = new List<Ingredient>()
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "Oua",
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Mere",
                }
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Menus).ReturnsDbSet(menu);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
        }
    }
}
