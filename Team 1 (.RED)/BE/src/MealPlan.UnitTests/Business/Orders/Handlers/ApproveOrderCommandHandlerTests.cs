using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
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
    public class ApproveOrderCommandHandlerTests
    {
        private ApproveOrderCommandHandler _handler;
        private Mock<MealPlanContext> _context;
        private ApproveOrderCommand _successfulCommand;
        private ApproveOrderCommand _noMoneyCommand;
        private ApproveOrderCommand _noOrderCommand;
        private ApproveOrderCommand _alreadyApprovedCommand;
        private ApproveOrderCommand _dateAlreadyPassedCommand;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new ApproveOrderCommandHandler(_context.Object);

            SetupContext();
            CreateCommands();
        }

        [TearDown]
        public void TearDown()
        {
            _handler = null;
            _context = null;
        }

        [Test]
        public async Task ShouldReturnTrue()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

            var result = await _handler.Handle(_successfulCommand, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenOrderDoesNotExist_ShouldThrowError()
        {
            Func<Task> action = async () => await _handler.Handle(_noOrderCommand, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order does not exist",
                        Code = (int)ErrorCode.OrderDoesNotExist
                    }));
        }

        [Test]
        public async Task WhenOrderIsAlreadyApproved_ShouldThrowError()
        {
            Func<Task> action = async () => await _handler.Handle(_alreadyApprovedCommand, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order has already been approved",
                        Code = (int)ErrorCode.OrderAlreadyApproved
                    }));
        }

        [Test]
        public async Task WhenUserDoesNotHaveEnoughMoney_ShouldThrowError()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            Func<Task> action = async () => await _handler.Handle(_noMoneyCommand, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "User doesn't have enough money to purchase",
                        Code = (int)ErrorCode.UserDoesNotHaveEnoughMoney
                    }));
        }

        [Test]
        public async Task WhenStartDateHasAlreadyPassed_ShouldThrowError()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            Func<Task> action = async () => await _handler.Handle(_dateAlreadyPassedCommand, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order start date has already passed",
                        Code = (int)ErrorCode.OrderStartDatePassed
                    }));
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Ana",
                    LastName = "Mere",
                    Email = "anaaremere@gmail.com",
                    Password = "SUPERSECURE",
                    RoleId = Role.User,
                    WalletBalance = 3000
                },
                new User
                {
                    Id = 2,
                    FirstName = "Paul",
                    LastName = "Pere",
                    Email = "paularepere@gmail.com",
                    Password = "SUPERSECURE1",
                    RoleId = Role.User,
                    WalletBalance = 0
                }
            };

            var menus = new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    Name = "Merele",
                    Description = "Merele Anei",
                    CategoryId = MenuCategory.Vegetarian,
                    Meals = new List<Meal>
                    {
                        new Meal
                        {
                            Id = 1,
                            Name = "Papara de mere",
                            Price = 15,
                            MealTypeId = MealType.Breakfast
                        },
                        new Meal
                        {
                            Id = 2,
                            Name = "Ciorba de mere",
                            Price = 15,
                            MealTypeId = MealType.Lunch
                        },
                        new Meal
                        {
                            Id = 3,
                            Name = "Friptura de mere",
                            Price = 15,
                            MealTypeId = MealType.Dinner
                        },
                        new Meal
                        {
                            Id = 4,
                            Name = "Prajitura de mere",
                            Price = 15,
                            MealTypeId = MealType.Dessert
                        },
                        new Meal
                        {
                            Id = 5,
                            Name = "Mar",
                            Price = 15,
                            MealTypeId = MealType.Snack
                        }
                    }
                }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id= 1,
                    Menu = menus[0],
                    MenuId = 1,
                    UserId = 1,
                    User = users[0],
                    Address = "gradina Anei",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1),
                    OrderStatusId = OrderStatus.Pending
                },
                new Order
                {
                    Id= 2,
                    Menu = menus[0],
                    MenuId = 1,
                    UserId = 2,
                    User = users[1],
                    Address = "gradina lui Paul",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1),
                    OrderStatusId = OrderStatus.Pending
                },
                new Order
                {
                    Id= 4,
                    Menu = menus[0],
                    MenuId = 1,
                    UserId = 1,
                    User = users[0],
                    Address = "gradina lui Paul",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1),
                    OrderStatusId = OrderStatus.Approved
                },
                new Order
                {
                    Id= 5,
                    Menu = menus[0],
                    MenuId = 1,
                    UserId = 1,
                    User = users[0],
                    Address = "gradina lui Paul",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    OrderStatusId = OrderStatus.Pending
                }
            };

            _context.Setup(m => m.Users).ReturnsDbSet(users);
            _context.Setup(m => m.Menus).ReturnsDbSet(menus);
            _context.Setup(m => m.Orders).ReturnsDbSet(orders);
        }
        private void CreateCommands()
        {
            _successfulCommand = new ApproveOrderCommand
            {
                OrderID = 1
            };

            _noMoneyCommand = new ApproveOrderCommand
            {
                OrderID = 2
            };

            _noOrderCommand = new ApproveOrderCommand
            {
                OrderID = 3
            };

            _alreadyApprovedCommand = new ApproveOrderCommand
            {
                OrderID = 4
            };

            _dateAlreadyPassedCommand = new ApproveOrderCommand
            {
                OrderID = 5
            };
        }
    }
}