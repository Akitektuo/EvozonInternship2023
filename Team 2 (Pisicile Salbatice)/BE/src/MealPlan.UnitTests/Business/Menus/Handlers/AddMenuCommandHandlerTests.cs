using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Menus.Commands;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Models;
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
    public class AddMenuCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddMenuCommandHandler _handler;
        private AddMenuCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddMenuCommandHandler(_context.Object);

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
            _request.MenuName = "M1";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.MenuNameDuplicated);
        }

        [Test]
        public async Task WhenUsingInvalidRecipe_ShouldThrowExceptions()
        {
            _request.Meals[0].RecipeId = 10;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.RecipeNotFound);
        }

        [Test]
        public async Task WhenUsingAlreadyUsedRecipeId_ShouldThrowExceptions()
        {
            _request.Meals[0].RecipeId = 6;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.RecipeAlredyUsed);
        }

        [Test]
        public async Task WhenUsingDuplicateMealName_ShouldThrowExceptions()
        {
            _request.Meals[0].Name = "M1";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.MealNameDuplicated);
        }
        private void CreateRequest()
        {
            _request = new AddMenuCommand
            {
                MenuName = "M2",
                MenuTypeId = MenuType.Fitness,
                Meals = new List<MealModel>()
                {
                    new MealModel{ 
                        Name = "M10",
                        Description = "d1",
                        Price = 20,
                        MealTypeId = MealType.Breakfast,
                        RecipeId = 1
                    },
                    new MealModel{
                        Name = "M12",
                        Description = "d2",
                        Price = 20,
                        MealTypeId = MealType.Lunch,
                        RecipeId = 2
                    },
                    new MealModel{
                        Name = "M13",
                        Description = "d3",
                        Price = 20,
                        MealTypeId = MealType.Dinner,
                        RecipeId = 3
                    },
                    new MealModel{
                        Name = "M14",
                        Description = "d4",
                        Price = 20,
                        MealTypeId = MealType.Dessert,
                        RecipeId = 4
                    },
                    new MealModel{
                        Name = "M15",
                        Description = "d5",
                        Price = 20,
                        MealTypeId = MealType.Snack,
                        RecipeId = 5
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

            _context.Setup(c => c.Users).ReturnsDbSet(users);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Menus).ReturnsDbSet(menu);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
        }
    }
}
