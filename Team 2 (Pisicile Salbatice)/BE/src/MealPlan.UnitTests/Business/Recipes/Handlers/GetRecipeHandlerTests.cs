using AutoFixture;
using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Handlers;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Recipes.Handlers
{
    [TestFixture]
    public class GetRecipeHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetRecipeHandler _handler;
        private GetRecipeQuery _request;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetRecipeHandler(_context.Object);
            _fixture = new Fixture();

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
        public async Task ShouldReturnCorrectRecipe()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            result.Should().BeEquivalentTo(new RecipeModel {Id = 1, Description = "Description", Name = "Recipe", Ingredients = new List<string>() });
        }

        [Test]
        public async Task WhenUsingInvalidId_ShouldThrowException()
        {
            _request.RecipeId = 3;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.RecipeNotFound);
        }

        private void SetupContext()
        {
            var recipes = new List<Recipe>
            {
                new Recipe {Id = 1, Description = "Description", Name = "Recipe", Ingredients = new List<Ingredient>()},
                new Recipe {Id = 2, Description = "Description2", Name = "Recipe2", Ingredients = new List<Ingredient>()},
            };

            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
        }

        private void CreateRequest()
        {
            _request = new GetRecipeQuery { RecipeId = 1 };
        }
    }
}
