using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using System.Linq;
using MealPlan.Business.Menus.Models;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    [TestFixture]
    public class GetAllMenusQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetAllMenusQueryHandler _handler;
        private GetAllMenusQuery _request;

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
        public async Task ShouldReturnCorrectListOfMenus()
        {
            _request.Category = MenuType.Fitness;

            var result = await _handler.Handle(_request, new CancellationToken());

            result.MenusList.Count.Should().Be(1);
            result.TotalMenusCount.Should().Be(1);
            result.MenusList.ElementAt(0).Should().BeEquivalentTo(new MenuModel { Id = 1, Name = "Menu1", Price = (float)31.6 });
        }

        [Test]
        public async Task ShouldReturnCorrectListOfMenusDifferentPaging()
        {
            _request.Category = MenuType.Vegetarian;

            _request.PageSize = 2;
            _request.PageNumber = 1;
            var result = await _handler.Handle(_request, new CancellationToken());

            result.MenusList.Count.Should().Be(1);
            result.TotalMenusCount.Should().Be(1);
            result.MenusList.ElementAt(0).Should().BeEquivalentTo(new MenuModel { Id = 4, Name = "Menu4", Price = (float)11 });
        }

        [Test]
        public async Task WhenRequestingAnInvalidPage_ShouldReturnEmptyList()
        {
            _request.PageNumber = 10;
            var result = await _handler.Handle(_request, new CancellationToken());

            result.MenusList.Count().Should().Be(0);
            result.TotalMenusCount.Should().Be(1);
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

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
        }

        private void CreateRequest()
        {
            _request = new GetAllMenusQuery { PageNumber = 1, PageSize = 3, Category = MenuType.Gym};
        }
    }
}
