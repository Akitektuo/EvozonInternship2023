using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Ingredients.Commands;
using MealPlan.Business.Ingredients.Handlers;
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

namespace MealPlan.UnitTests.Business.Ingredients.Handlers
{
    public class AddIngredientCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddIngredientCommandHandler _handler;
        private AddIngredientCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddIngredientCommandHandler(_context.Object);

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
            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().NotThrowAsync();
        }

        [Test]
        public async Task WhenUsingDuplicateMenuName_ShouldThrowExceptions()
        {
            _request.IngredientName = "ingredient1";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.IngredientAlreadyExists);
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
            _request = new AddIngredientCommand { IngredientName = "new ingredient" };
        }
    }
}
