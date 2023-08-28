using FluentAssertions;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MealPlan.Business.Shared;
using MealPlan.Data;
using MealPlan.Data.Models.Orders;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Orders.Handlers
{
    [TestFixture]
    public class GetOrdersQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetOrdersQueryHandler _handler;
        private GetOrdersQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetOrdersQueryHandler(_context.Object);

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
        public async Task ShouldReturnCorrectOrders()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel> {
                new OrderModel { Id = 2,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"},
                new OrderModel { Id = 1,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"}});
        }

        [Test]
        public async Task WhenNoStatusIsGiven_ShouldReturnCorrectOrders()
        {
            _request.FiltrationModel.ColumnClauses = new List<ColumnClauseModel>();

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel> 
            {
                new OrderModel { Id = 2,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"},
                new OrderModel { Id = 1,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"},
                new OrderModel { Id = 3, MenuName = "menu", EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, StatusId = Status.WaitingForApproval, UserEmail = "u3@email.com" }
            });
        }

        [Test]
        public async Task WhenNoSearchTextIsGiven_ShouldReturnCorrectOrders()
        {
            _request.FiltrationModel.SearchText = null;

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel> 
            {
                new OrderModel { Id = 2,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"},
                new OrderModel { Id = 1,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"}
            });
        }

        [Test]
        public async Task WhenNoOrderByModelsIsGiven_ShouldReturnCorrectOrders()
        {
            _request.FiltrationModel.OrderByModels = new List<SortModel>();

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel> 
            {
                new OrderModel { Id = 1,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"},
                new OrderModel { Id = 2,  EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"} 
            });
        }

        [Test]
        public async Task WhenDefaultValuesAreUsed_ShouldReturnCorrectOrders()
        {
            _request.FiltrationModel.SearchText = string.Empty;
            _request.FiltrationModel.ColumnClauses = new List<ColumnClauseModel>();
            _request.FiltrationModel.OrderByModels = new List<SortModel>();

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel>
            {
                new OrderModel { Id = 1, EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"},
                new OrderModel { Id = 2, EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"},
                new OrderModel { Id = 3, MenuName = "menu", EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, StatusId = Status.WaitingForApproval, UserEmail = "u3@email.com" }
            });
        }

        [Test]
        public async Task WhenDifferentPagination_ShouldReturnCorrectOrders()
        {
            _request.FiltrationModel.SearchText = string.Empty;
            _request.FiltrationModel.ColumnClauses = new List<ColumnClauseModel>();
            _request.FiltrationModel.OrderByModels = new List<SortModel>();
            _request.PageSize = 2;

            var result = await _handler.Handle(_request, new CancellationToken());

            result.Orders.Should().BeEquivalentTo(new List<OrderModel>
            {
                new OrderModel { Id = 1, EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u2@email.com"},
                new OrderModel { Id = 2, EndDate = DateTime.MinValue, StartDate = DateTime.MinValue, MenuName = "menu", StatusId = Status.Approved, UserEmail = "u1@email.com"},
            });
        }

        private void SetupContext()
        {
            List<Order> orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    MenuId = 1,
                    MenuName = "menu",
                    EndDate = DateTime.MinValue,
                    StartDate = DateTime.MinValue,
                    ShippingAddress = "home",
                    StatusId = Status.Approved,
                    PhoneNumber = "0736542835",
                    UserEmail = "u2@email.com"
                },
                new Order
                {
                    Id = 2,
                    MenuId = 1,
                    MenuName = "menu",
                    EndDate = DateTime.MinValue,
                    StartDate = DateTime.MinValue,
                    ShippingAddress = "home",
                    StatusId = Status.Approved,
                    PhoneNumber = "0735642800",
                    UserEmail = "u1@email.com"
                },
                new Order
                {
                    Id = 3,
                    MenuId = 1,
                    MenuName = "menu",
                    EndDate = DateTime.MinValue,
                    StartDate = DateTime.MinValue,
                    ShippingAddress = "home",
                    StatusId = Status.WaitingForApproval,
                    PhoneNumber = "073463472",
                    UserEmail = "u3@email.com"
                }
            };

            _context.Setup(x => x.Orders).ReturnsDbSet(orders);
        }

        private void CreateRequest()
        {
            _request = new GetOrdersQuery
            {
                PageNumber = 1,
                PageSize = 3,
                FiltrationModel = new FiltrationModel
                {
                    ColumnClauses = new List<ColumnClauseModel> { new ColumnClauseModel { ColumnName = "StatusId", Value = Status.Approved.ToString() } },
                    SearchText = "me",
                    OrderByModels = new List<SortModel>
                    {
                        new SortModel
                        {
                            Column = "UserEmail",
                            SortOrder = "asc"
                        }
                    }
                }
            };
        }
    }
}
