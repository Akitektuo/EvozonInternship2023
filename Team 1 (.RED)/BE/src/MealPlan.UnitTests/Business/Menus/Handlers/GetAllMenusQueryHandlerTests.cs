using FluentAssertions;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Business.Utils;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class GetAllMenusQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetAllMenusQueryHandler _handler;
        private GetAllMenusQuery _successfulQuery;
        private GetAllMenusQuery _failureQuery;
        private GetAllMenusQuery _emptyResultQuery;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetAllMenusQueryHandler(_context.Object);

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
        public async Task ShouldReturnPageOfMenus()
        {
            var result = await _handler.Handle(_successfulQuery, new CancellationToken());

            var menusToReturn = new List<MenuOverview>
            {
                new MenuOverview
                {
                    Id = 1,
                    Description = "description1",
                    Name = "menu1",
                    Price = 7
                }
            };

            var pagedMenusResult = new PaginationModel<MenuOverview> { Items = menusToReturn, TotalRecords = 1 };

            result.TotalRecords.Should().Be(pagedMenusResult.TotalRecords);
            result.Items.Should().BeEquivalentTo(pagedMenusResult.Items);
        }

        [Test]
        public async Task WhenPageNumberDoesntExist_ShouldReturnEmptyList()
        {
            var result = await _handler.Handle(_failureQuery, new CancellationToken());

            var menusToReturn = new List<MenuOverview>();

            var pagedMenusResult = new PaginationModel<MenuOverview> { Items = menusToReturn, TotalRecords = 1 };

            result.TotalRecords.Should().Be(pagedMenusResult.TotalRecords);
            result.Items.Should().BeEquivalentTo(pagedMenusResult.Items);
        }

        [Test]
        public async Task WhenCategoryIsEmpty_ShouldReturnEmptyListAndNoRecords()
        {
            var result = await _handler.Handle(_emptyResultQuery, new CancellationToken());

            var menusToReturn = new List<MenuOverview>();

            var pagedMenusResult = new PaginationModel<MenuOverview> { Items = menusToReturn, TotalRecords = 0 };

            result.TotalRecords.Should().Be(pagedMenusResult.TotalRecords);
            result.Items.Should().BeEquivalentTo(pagedMenusResult.Items);
        }

        private void CreateRequest()
        {
            _successfulQuery = new GetAllMenusQuery { PageNumber = 1, PageSize = 5, CategoryId = MenuCategory.Vegetarian };

            _failureQuery = new GetAllMenusQuery { PageNumber = 15, PageSize = 20 , CategoryId = MenuCategory.Vegetarian };

            _emptyResultQuery = new GetAllMenusQuery { PageNumber = 1, PageSize = 5, CategoryId = MenuCategory.Fitness };
        }

        private void SetupContext()
        {
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
                    CategoryId = MenuCategory.Vegetarian
                },
                new Menu
                {
                    Id = 2,
                    Name = "menu2",
                    Description = "description2",
                    Category = new MenuCategoryLookup
                    {
                        Id = MenuCategory.Gym,
                        Name = "Gym"
                    },
                    CategoryId = MenuCategory.Gym
                }
            };

            var meals = new List<Meal>
            {
                new Meal
                {
                    Id = 1,
                    Name = "meal1",
                    Price = 1,
                    MenuId = 1,
                    Menu = menus[0]
                },
                new Meal
                {
                    Id = 2,
                    Name = "meal2",
                    Price = 1,
                    MenuId = 1,
                    Menu = menus[0]
                },
                new Meal
                {
                    Id = 3,
                    Name = "meal3",
                    Price = 3,
                    MenuId = 1,
                    Menu = menus[0]
                },
                new Meal
                {
                    Id = 4,
                    Name = "meal4",
                    Price = 1,
                    MenuId = 1,
                    Menu = menus[0]
                },
                new Meal
                {
                    Id = 5,
                    Name = "meal5",
                    Price = 1,
                    MenuId = 1,
                    Menu = menus[0]
                },
                new Meal
                {
                    Id = 6,
                    Name = "meal6",
                    Price = 1,
                    MenuId = 2,
                    Menu = menus[1]
                },
                new Meal
                {
                    Id = 7,
                    Name = "meal7",
                    Price = 1,
                    MenuId = 2,
                    Menu = menus[1]
                },
                new Meal
                {
                    Id = 8,
                    Name = "meal8",
                    Price = 3,
                    MenuId = 2,
                    Menu = menus[1]
                },
                new Meal
                {
                    Id = 9,
                    Name = "meal9",
                    Price = 1,
                    MenuId = 2,
                    Menu = menus[1]
                },
                new Meal
                {
                    Id = 10,
                    Name = "meal10",
                    Price = 1,
                    MenuId = 2,
                    Menu = menus[1]
                }
            };

            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
        }
    }
}