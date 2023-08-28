using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Business.Services;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Orders;
using MealPlan.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Orders.Handlers
{
    [TestFixture]
    public class UpdateOrderStatusCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private UpdateOrderStatusCommandHandler _handler;
        private Mock<OrderStatusService> _service;
        private UpdateOrderStatusCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _service = new Mock<OrderStatusService>(_context.Object);
            _handler = new UpdateOrderStatusCommandHandler(_service.Object);

            CreateRequest();
            SetupContext();
        }

        [Test]
        public async Task WhenRequestStatusIsApprovedAndValidOrder_ShouldReturnTrue()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenRequestStatusIsRejected_ShouldReturnTrue()
        {
            _request.StatusId = Status.Rejected;

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenRequestStatusIsWaitingForApproval_ShouldThrowError()
        {
            _request.StatusId = Status.WaitingForApproval;

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.CannotResetStatus);
        }

        [Test]
        public async Task WhenRequestStatusIsNotInEnum_ShouldThrowNotImplementedException()
        {
            _request.StatusId = (Status)0;

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<NotImplementedException>();
        }

        [Test]
        public async Task WhenRequestStatusIsApprovedAndOrderIsAlreadyApproved_ShouldThrowError()
        {
            _request.OrderId = await _context.Object.Orders.Where(o => o.StatusId == Status.Approved).Select(o => o.Id).FirstOrDefaultAsync();

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.CannotApproveOrder);
        }

        [Test]
        public async Task WhenRequestStatusIsRejectedAndOrderIsAlreadyRejected_ShouldThrowError()
        {
            _request.OrderId = await _context.Object.Orders.Where(o => o.StatusId == Status.Rejected).Select(o => o.Id).FirstOrDefaultAsync();
            _request.StatusId = Status.Rejected;

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.CannotRejectOrder);
        }

        [Test]
        public async Task WhenOrderIdIsInvalid_ShouldThrowError()
        {
            _request.OrderId = 0;

            var action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.OrderDoesNotExist);
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
            var menus = new List<Menu>
            {
                new Menu { Id = 1, Name = "Menu1", MenuTypeId = MenuType.Fitness, MenuType = menuLookup[0] },
                new Menu { Id = 2, Name = "Menu2", MenuTypeId = MenuType.Gym, MenuType = menuLookup[1] },
                new Menu { Id = 3, Name = "Menu3", MenuTypeId = MenuType.FoodLover, MenuType = menuLookup[2] },
                new Menu { Id = 4, Name = "Menu4", MenuTypeId = MenuType.Vegetarian, MenuType = menuLookup[3] },
            };

            var meals = new List<Meal>
            {
                new Meal { Id = 1, Name = "Meal1", Description = "GoodMeal1", Price = (float)5.3, MealTypeId = MealType.Breakfast, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 2, Name = "Meal2", Description = "GoodMeal2", Price = (float)7.3, MealTypeId = MealType.Lunch, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 3, Name = "Meal3", Description = "GoodMeal3", Price = (float)12 , MealTypeId = MealType.Dinner, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 4, Name = "Meal4", Description = "GoodMeal4", Price = (float)4 , MealTypeId = MealType.Dessert, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 5, Name = "Meal5", Description = "GoodMeal5", Price = (float)3 , MealTypeId = MealType.Snack, MenuId = 1, Menu = menus[0]},
                new Meal { Id = 6, Name = "Meal6", Description = "GoodMeal6", Price = (float)6 , MealTypeId = MealType.Breakfast, MenuId = 2, Menu = menus[1]},
                new Meal { Id = 7, Name = "Meal7", Description = "GoodMeal7", Price = (float)10 , MealTypeId = MealType.Lunch, MenuId = 2, Menu = menus[1]},
                new Meal { Id = 8, Name = "Meal8", Description = "GoodMeal8", Price = (float)11 , MealTypeId = MealType.Dinner, MenuId = 2, Menu = menus[1]},
                new Meal { Id = 9, Name = "Meal9", Description = "GoodMeal9", Price = (float)5 , MealTypeId = MealType.Dessert, MenuId = 2, Menu = menus[1]},
                new Meal { Id = 10, Name = "Meal10", Description = "GoodMeal10", Price = (float)2 , MealTypeId = MealType.Snack, MenuId = 2, Menu = menus[1]},
                new Meal { Id = 11, Name = "Meal11", Description = "GoodMeal11", Price = (float)1 , MealTypeId = MealType.Breakfast, MenuId = 3, Menu = menus[2]},
                new Meal { Id = 12, Name = "Meal12", Description = "GoodMeal12", Price = (float)2 , MealTypeId = MealType.Lunch, MenuId = 3, Menu = menus[2]},
                new Meal { Id = 13, Name = "Meal13", Description = "GoodMeal13", Price = (float)3 , MealTypeId = MealType.Dinner, MenuId = 3, Menu = menus[2]},
                new Meal { Id = 14, Name = "Meal14", Description = "GoodMeal14", Price = (float)4 , MealTypeId = MealType.Dessert, MenuId = 3, Menu = menus[2]},
                new Meal { Id = 15, Name = "Meal15", Description = "GoodMeal15", Price = (float)5 , MealTypeId = MealType.Snack, MenuId = 3, Menu = menus[2]},
                new Meal { Id = 16, Name = "Meal16", Description = "GoodMeal16", Price = (float)11 , MealTypeId = MealType.Snack, MenuId = 4, Menu = menus[3]},
            };

            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Joe", LastName = "Jonas", Email = "joe@email.com", Password = "joejoe", Balance = 10000, RoleId = Role.Admin},
            };

            var orders = new List<Order>
            {
                new Order { Id = 1, Menu = menus[0], MenuName = menus[0].Name, MenuId = menus[0].Id, User = users[0], UserId = users[0].Id, UserEmail = users[0].Email, StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(20), PhoneNumber = "fffdas", ShippingAddress = "fsdafdas", StatusId = Status.WaitingForApproval, TotalPrice = 100},
                new Order { Id = 2, Menu = menus[1], MenuName = menus[1].Name, MenuId = menus[1].Id, User = users[0], UserId = users[0].Id, UserEmail = users[0].Email, StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(20), PhoneNumber = "fffdas", ShippingAddress = "fsdafdas", StatusId = Status.Approved, TotalPrice = 100},
                new Order { Id = 3, Menu = menus[2], MenuName = menus[2].Name, MenuId = menus[2].Id, User = users[0], UserId = users[0].Id, UserEmail = users[0].Email, StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(20), PhoneNumber = "fffdas", ShippingAddress = "fsdafdas", StatusId = Status.Rejected, TotalPrice = 100},
            };

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Users).ReturnsDbSet(users);
            _context.Setup(c => c.Orders).ReturnsDbSet(orders);

            _context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        }

        private void CreateRequest()
        {
            _request = new UpdateOrderStatusCommand
            {
                OrderId = 1,
                StatusId = Status.Approved
            };
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }
    }
}
