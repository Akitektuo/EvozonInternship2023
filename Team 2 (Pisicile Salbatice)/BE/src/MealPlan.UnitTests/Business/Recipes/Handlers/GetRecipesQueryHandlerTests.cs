using MealPlan.Business.Recipes.Handlers;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using MealPlan.Business.Recipes.Models;
using MealPlan.Data.Models.Meals;

namespace MealPlan.UnitTests.Business.Recipes.Handlers
{
    [TestFixture]
    public class GetRecipesQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetRecipesQueryHandler _handler;
        private GetRecipesQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetRecipesQueryHandler(_context.Object);

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
        public async Task WhenFirstPage_ShouldReturnCorrectResponse()
        {
            var recipes = new List<RecipeResponse>
            {
                new RecipeResponse {Id = 1, Description = "Description1", Name = "Recipe1"},
                new RecipeResponse {Id = 2, Description = "Description2", Name = "Recipe2"},
                new RecipeResponse {Id = 3, Description = "Description3", Name = "Recipe3"}
            };

            var result = await _handler.Handle(_request, new CancellationToken());

            result.RecipesList.Should().BeEquivalentTo(recipes);
            result.TotalRecipesCount.Should().Be(4);
        }

        [Test]
        public async Task WhenSecondPage_ShouldReturnCorrectResponse()
        {
            _request.PageNumber = 2;

            var recipes = new List<RecipeResponse>
            {
                new RecipeResponse {Id = 4, Description = "Description4", Name = "Recipe4"},
            };

            var result = await _handler.Handle(_request, new CancellationToken());

            result.RecipesList.Should().BeEquivalentTo(recipes);
            result.TotalRecipesCount.Should().Be(4);
        }

        [Test]
        public async Task WhenInvalidPage_ShouldReturnCorrectResponse()
        {
            _request.PageNumber = 3;

            var recipes = new List<RecipeResponse>();

            var result = await _handler.Handle(_request, new CancellationToken());

            result.RecipesList.Should().BeEquivalentTo(recipes);
            result.TotalRecipesCount.Should().Be(4);
        }

        [Test]
        public async Task WhenFilterUnusedRecipesIsTrue_ShouldReturnCorrectResponse()
        {
            _request.FilterUnusedRecipes = true;

            var recipes = new List<RecipeResponse>
            {
                new RecipeResponse {Id = 2, Description = "Description2", Name = "Recipe2"},
                new RecipeResponse {Id = 3, Description = "Description3", Name = "Recipe3"},
                new RecipeResponse {Id = 4, Description = "Description4", Name = "Recipe4"}
            };

            var result = await _handler.Handle(_request, new CancellationToken());

            result.RecipesList.Should().BeEquivalentTo(recipes);
            result.TotalRecipesCount.Should().Be(3);
        }

        private void SetupContext()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                    Id = 1,
                    Name = "Meal",
                    Description = "Description",
                    Price = 15,
                    MealTypeId = MealType.Breakfast,
                    RecipeId = 1
                }
            };

            var recipes = new List<Recipe>
            {
                new Recipe {Id = 1, Description = "Description1", Name = "Recipe1", Ingredients = new List<Ingredient>(), Meal = meals[0]},
                new Recipe {Id = 2, Description = "Description2", Name = "Recipe2", Ingredients = new List<Ingredient>()},
                new Recipe {Id = 3, Description = "Description3", Name = "Recipe3", Ingredients = new List<Ingredient>()},
                new Recipe {Id = 4, Description = "Description4", Name = "Recipe4", Ingredients = new List<Ingredient>()}
            };

            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
        }

        private void CreateRequest()
        {
            _request = new GetRecipesQuery { PageNumber = 1, PageSize = 3, FilterUnusedRecipes = false };
        }
    }
}
