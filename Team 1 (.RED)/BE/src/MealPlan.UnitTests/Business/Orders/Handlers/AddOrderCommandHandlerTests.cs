using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Orders;
using MealPlan.Data.Models.Users;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Orders.Handlers
{
    [TestFixture]
    public class AddOrderCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddOrderCommandHandler _handler;
        private AddOrderCommand _successfulRequest;
        private AddOrderCommand _userDoesNotExistRequest;
        private AddOrderCommand _menuDoesNotExistRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddOrderCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldAddOrder()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenUserDoesNotExist_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_userDoesNotExistRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "User not found",
                        Code = (int)ErrorCode.OrderUserNotFound
                    }));
        }

        [Test]
        public async Task WhenMenuDoesNotExist_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_menuDoesNotExistRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Menu not found",
                        Code = (int)ErrorCode.OrderMenuNotFound
                    }));
        }

        private void CreateCommands() 
        {
            _successfulRequest = new AddOrderCommand
            {
                UserEmail = "user@email.com",
                Address = "address",
                PhoneNumber = "3423432434",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                MenuId = 1
            };

            _userDoesNotExistRequest = new AddOrderCommand
            {
                UserEmail = "phantomuser@email.com",
                Address = "address",
                PhoneNumber = "3423432434",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                MenuId = 1
            };

            _menuDoesNotExistRequest = new AddOrderCommand
            {
                UserEmail = "user@email.com",
                Address = "address",
                PhoneNumber = "3423432434",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                MenuId = 1234567
            };
        }

        private void SetupContext()
        {
            var users = new List<User> 
            { 
                new User 
                { 
                    Id = 1, 
                    Email = "user@email.com" 
                } 
            };

            var menus = new List<Menu> 
            { 
                new Menu
                {
                    Id = 1
                }
            };

            var orders = new List<Order> { };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Orders).ReturnsDbSet(orders);
        }
    }
}