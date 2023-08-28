using FluentAssertions;
using MealPlan.Business.Orders.Handlers;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
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
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Orders.Handlers
{
    [TestFixture]
    public class GetAllOrdersQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetAllOrdersQueryHandler _handler;
        private GetAllOrdersQuery _defaultValuesQuery;
        private GetAllOrdersQuery _orderingDescWithoutFilteringQuery;
        private GetAllOrdersQuery _orderingAscWithoutFilteringQuery;
        private GetAllOrdersQuery _filteringWithoutOrderingQuery;
        private GetAllOrdersQuery _filteringAndOrderingQuery;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetAllOrdersQueryHandler(_context.Object);

            CreateRequest();
            SetupContext();
        }

        [Test]
        public async Task ShouldReturnCorrectPaginationModelFilteredByStartDate()
        {
            var result = await _handler.Handle(_defaultValuesQuery, new CancellationToken());

            var orders = new List<OrderDetails>
            {
                new OrderDetails
                {
                    UserEmail = "paularepere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 12),
                    EndDate = new DateTime(2023, 10, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 13),
                    EndDate = new DateTime(2023, 10, 23),
                    OrderStatus = OrderStatus.Approved.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                }
            };

            result.TotalRecords.Should().Be(3);
            result.Items.Should().BeEquivalentTo(orders);
        }

        [Test]
        public async Task ShouldReturnCorrectPaginationModelOrderedDescBySelectedColumns()
        {
            var result = await _handler.Handle(_orderingDescWithoutFilteringQuery, new CancellationToken());

            var orders = new List<OrderDetails>
            {
                new OrderDetails
                {
                    UserEmail = "paularepere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 12),
                    EndDate = new DateTime(2023, 10, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                }
            };

            result.TotalRecords.Should().Be(3);
            result.Items.Should().BeEquivalentTo(orders);
        }

        [Test]
        public async Task ShouldReturnCorrectPaginationModelOrderedAscBySelectedColumns()
        {
            var result = await _handler.Handle(_orderingAscWithoutFilteringQuery, new CancellationToken());

            var orders = new List<OrderDetails>
            {
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 13),
                    EndDate = new DateTime(2023, 10, 23),
                    OrderStatus = OrderStatus.Approved.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "paularepere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 12),
                    EndDate = new DateTime(2023, 10, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                }
            };

            result.TotalRecords.Should().Be(3);
            result.Items.Should().BeEquivalentTo(orders);
        }


        [Test]
        public async Task ShouldReturnCorrectPaginationModelFilteredBySearchText()
        {
            var result = await _handler.Handle(_filteringWithoutOrderingQuery, new CancellationToken());

            var orders = new List<OrderDetails>
            {
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 13),
                    EndDate = new DateTime(2023, 10, 23),
                    OrderStatus = OrderStatus.Approved.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                }
            };

            result.TotalRecords.Should().Be(2);
            result.Items.Should().BeEquivalentTo(orders);
        }

        [Test]
        public async Task ShouldReturnCorrectPaginationModelFilteredAndOrdered()
        {
            var result = await _handler.Handle(_filteringAndOrderingQuery, new CancellationToken());

            var orders = new List<OrderDetails>
            {
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
                    OrderStatus = OrderStatus.Pending.ToString()
                },
                new OrderDetails
                {
                    UserEmail = "anaaremere@gmail.com",
                    MenuName = "Merele",
                    StartDate = new DateTime(2023, 10, 13),
                    EndDate = new DateTime(2023, 10, 23),
                    OrderStatus = OrderStatus.Approved.ToString()
                }
            };

            result.TotalRecords.Should().Be(2);
            result.Items.Should().BeEquivalentTo(orders);
        }

        private void SetupContext()
        {
            var orderStatusLookup = new List<OrderStatusLookup>
            {
                new OrderStatusLookup
                {
                    Id = OrderStatus.Approved,
                    Name = "Approved"
                },
                new OrderStatusLookup
                {
                    Id = OrderStatus.Denied,
                    Name = "Denied"
                },
                new OrderStatusLookup
                {
                    Id = OrderStatus.Pending,
                    Name = "Pending"
                }
            };

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
                    WalletBalance = 5
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
                    StartDate = new DateTime(2023, 11, 15),
                    EndDate = new DateTime(2023, 12, 22),
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
                    StartDate = new DateTime(2023, 10, 12),
                    EndDate = new DateTime(2023, 10, 22),
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
                    StartDate = new DateTime(2023, 10, 13),
                    EndDate = new DateTime(2023, 10, 23),
                    OrderStatusId = OrderStatus.Approved
                }
            };

            _context.Setup(m => m.OrderStatusLookup).ReturnsDbSet(orderStatusLookup);
            _context.Setup(m => m.Users).ReturnsDbSet(users);
            _context.Setup(m => m.Menus).ReturnsDbSet(menus);
            _context.Setup(m => m.Orders).ReturnsDbSet(orders);
        }

        private void CreateRequest()
        {
            _defaultValuesQuery = new GetAllOrdersQuery
            {
                PageNumber = 1,
                PageSize = 10,
                FilterByStatus = 0,
                SearchText = "",
                OrderByDescending = false,
                OrderByColumns = ""
            };

            _orderingDescWithoutFilteringQuery = new GetAllOrdersQuery
            {
                PageNumber = 1,
                PageSize = 2,
                FilterByStatus = 0,
                SearchText = "",
                OrderByDescending = true,
                OrderByColumns = "UserEmail, MenuName"
            };

            _orderingAscWithoutFilteringQuery = new GetAllOrdersQuery
            {
                PageNumber = 1,
                PageSize = 3,
                FilterByStatus = 0,
                SearchText = "",
                OrderByDescending = false,
                OrderByColumns = "UserEmail, MenuName"
            };

            _filteringWithoutOrderingQuery = new GetAllOrdersQuery
            {
                PageNumber = 1,
                PageSize = 3,
                FilterByStatus = 0,
                SearchText = "mere",
                OrderByDescending = false,
                OrderByColumns = ""
            };

            _filteringAndOrderingQuery = new GetAllOrdersQuery
            {
                PageNumber = 1,
                PageSize = 9,
                FilterByStatus = 0,
                SearchText = "mere",
                OrderByDescending = true,
                OrderByColumns = "StartDate"
            };
        }
    }
}