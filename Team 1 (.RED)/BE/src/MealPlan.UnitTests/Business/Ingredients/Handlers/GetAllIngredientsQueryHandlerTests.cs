using FluentAssertions;
using MealPlan.Business.Ingredients.Handlers;
using MealPlan.Business.Ingredients.Models;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Ingredients.Handlers
{
    [TestFixture]
    public class GetAllIngredientsQueryHandlerTests
    {
        private GetAllIngredientsQueryHandler _handler;
        private Mock<MealPlanContext> _context;
        private GetAllIngredientsQuery _query;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<MealPlanContext>();

            _handler = new GetAllIngredientsQueryHandler(_context.Object);

            _query = new GetAllIngredientsQuery();

            SetUpContext();
        }

        [TearDown]
        public void TearDown()
        {
            _context = null;

            _handler = null;
        }

        [Test]
        public async Task ShouldReturnListOfIngredientsOverview()
        {
            var result = await _handler.Handle(_query, new CancellationToken());

            var ingredients = new List<IngredientModel>
            {
                new IngredientModel
                {
                    Id = 1,
                    Name = "mar"
                },
                new IngredientModel
                {
                    Id = 2,
                    Name = "para"
                },
                new IngredientModel
                {
                    Id = 3,
                    Name = "banana"
                }
            };

            result.Should().BeEquivalentTo(ingredients);
        }

        private void SetUpContext()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "mar"
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "para"
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "banana"
                }
            };

            _context.Setup(m => m.Ingredients).ReturnsDbSet(ingredients);
        }
    }
}