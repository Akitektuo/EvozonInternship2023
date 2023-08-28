using MealPlan.Business.Ingredients.Handlers;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using MealPlan.Business.Ingredients.Models;

namespace MealPlan.UnitTests.Business.Ingredients
{
    [TestFixture]
    public class GetIngredientsQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetIngredientsQueryHandler _handler;
        private GetIngredientsQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetIngredientsQueryHandler(_context.Object);

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
        public async Task ShouldReturnCorrectResponse()
        {
            var ingredients = new List<IngredientModel>
            {
                new IngredientModel {Id = 1, Name = "ingredient1"},
                new IngredientModel {Id = 2, Name = "ingredient2"},
                new IngredientModel {Id = 3, Name = "ingredient3"},
                new IngredientModel {Id = 4, Name = "ingredient4"}
            };

            var result = await _handler.Handle(_request, new CancellationToken());

            result.IngredientsList.Should().BeEquivalentTo(ingredients);
        }

        private void SetupContext()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient {Id = 1, Name = "ingredient1"},
                new Ingredient {Id = 2, Name = "ingredient2"},
                new Ingredient {Id = 3, Name = "ingredient3"},
                new Ingredient {Id = 4, Name = "ingredient4"}
            };

            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
        }

        private void CreateRequest()
        {
            _request = new GetIngredientsQuery();
        }
    }
}
