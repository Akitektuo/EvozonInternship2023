using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Commands;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Orders;
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
    public class DenyOrderCommandHandlerTests
    {
        private DenyOrderCommandHandler _handler;
        private Mock<MealPlanContext> _context;
        private DenyOrderCommand _successfulCommand;
        private DenyOrderCommand _alreadyApproved;
        private DenyOrderCommand _alreadyDenied;
        private DenyOrderCommand _orderDoesntExist;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new DenyOrderCommandHandler(_context.Object);

            SetupContext();
            CreateRequests();
        }

        [TearDown]
        public void Teardown()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnTrue()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _handler.Handle(_successfulCommand, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenOrderDoesntExist_ShouldThrowError()
        {
            Func<Task> action = async () => await _handler.Handle(_orderDoesntExist, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order doesn't exist",
                        Code = (int)ErrorCode.OrderDoesNotExist
                    }));
        }

        [Test]
        public async Task WhenOrderIsAlreadyApproved_ShouldThrowError()
        {
            Func<Task> action = async () => await _handler.Handle(_alreadyApproved, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order is already approved",
                        Code = (int)ErrorCode.OrderAlreadyApproved
                    }));
        }

        [Test]
        public async Task WhenOrderIsAlreadyDenied_ShouldThrowError()
        {
            Func<Task> action = async () => await _handler.Handle(_alreadyDenied, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Order is already denied",
                        Code = (int)ErrorCode.OrderAlreadyDenied
                    }));
        }

        private void SetupContext()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id= 1,
                    MenuId = 1,
                    UserId = 1,
                    Address = "gradina Anei",
                    OrderStatusId = OrderStatus.Pending
                },
                new Order
                {
                    Id= 2,
                    MenuId = 1,
                    UserId = 2,
                    Address = "gradina lui Paul",
                    OrderStatusId = OrderStatus.Denied
                },
                new Order
                {
                    Id= 4,
                    MenuId = 1,
                    UserId = 1,
                    Address = "gradina lui Paul",
                    OrderStatusId = OrderStatus.Approved
                }
            };

            _context.Setup(x => x.Orders).ReturnsDbSet(orders);
        }

        private void CreateRequests()
        {
            _successfulCommand = new DenyOrderCommand
            {
                OrderID = 1
            };

            _orderDoesntExist = new DenyOrderCommand
            {
                OrderID = 3
            };

            _alreadyDenied = new DenyOrderCommand
            {
                OrderID = 2
            };

            _alreadyApproved = new DenyOrderCommand 
            { 
                OrderID = 4 
            };
        }
    }
}