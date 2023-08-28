using FluentAssertions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MealPlan.Data.Models.Users;
using MealPlan.Business.Exceptions;

namespace MealPlan.UnitTests.Business.Orders.Handlers
{
    [TestFixture]
    public class AddOrderHandlerTests
    {

        private Mock<MealPlanContext> _context;
        private AddOrderCommandHandler _handler;
        private AddOrderCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddOrderCommandHandler(_context.Object);

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
        public async Task WhenNotSufficientFunds_ShouldThrowExceptions()
        {
            _request.EndDate = DateTime.Now.AddDays(25);
            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.InvalidUserWallet);
        }

        [Test]
        public async Task WhenMenuDoesNotExist_ShouldThrowExceptions()
        {
            _request.MenuId = 1000;
            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.AddOrderMenuNotFound);
        }

        [Test]
        public async Task WhenUserDoesNotExist_ShouldThrowExceptions()
        {
            _request.UserEmail = "haha@testnot.com";
            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.AddOrderUserNotFound);
        }

        private void CreateRequest()
        {
            _request = new AddOrderCommand
            {
                MenuId = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                PhoneNumber = "0772 289 312",
                ShippingAddress = "London Street",
                UserEmail = "joe@test.com"
            };
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

            var meals = new List<Meal>
            {
                new Meal { Id = 1, Name = "Meal1", Description = "GoodMeal1", Price =(float) 5.3, MealTypeId = mealLookup[0].Id, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 2, Name = "Meal2", Description = "GoodMeal2", Price = (float)7.3, MealTypeId = mealLookup[1].Id, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 3, Name = "Meal3", Description = "GoodMeal3", Price =(float) 12, MealTypeId = mealLookup[2].Id, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 4, Name = "Meal4", Description = "GoodMeal4", Price = (float)4 , MealTypeId = mealLookup[3].Id, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 5, Name = "Meal5", Description = "GoodMeal5", Price = (float)3 , MealTypeId = mealLookup[4].Id, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 6, Name = "Meal6", Description = "GoodMeal6", Price = (float)6 , MealTypeId = mealLookup[0].Id, MenuId = 2, Menu = menus[1]}
            };

            var users = new List<User>
            {
                new User { Id = 1, Email = "joe@test.com", Password = "joejoe", FirstName = "Joe", LastName = "Johnathan", RoleId = Role.User, Balance = 500 },
                new User { Id = 2, Email = "ana@test.com", Password = "anaana", FirstName = "Ana", LastName = "Johnathan", RoleId = Role.User }
            };

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }
    }
}
