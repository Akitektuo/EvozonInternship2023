using AutoFixture;
using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Commands;
using MealPlan.Business.Recipes.Handlers;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using MealPlan.Data.Models.Users;
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
    public class AddRecipeCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddRecipeCommandHandler _handler;
        private AddRecipeCommand _request;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddRecipeCommandHandler(_context.Object);
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
        public async Task WhenUsingInvalidName_ShouldThrowExceptions()
        {
            _request.Name = "test-recipe";

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should().ThrowAsync<CustomApplicationException>().Where(e => e.ErrorCode == ErrorCode.RecipeAlreadyExists);
        }

        [Test]
        public async Task WhenUsingValidInformation_ShouldReturnValidResponse()
        {
            var addRecipeModel = _fixture.Build<AddRecipeCommand>()
                .With(x => x.Name, "recipe-test-1")
                .With(x => x.Description, "lorem ipsum")
                .With(x => x.IngredientIds, new List<int> { 1, 2 })
                .Create();

            var result = await _handler.Handle(addRecipeModel, new CancellationToken());

            result.Should().BeTrue();
        }

        private void SetupContext()
        {
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "test-recipe", Description = "lorem ipsum" },
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Oua" },
                new Ingredient { Id = 2, Name = "Lapte" }
            };

            var users = new List<User>
            {
                new User { Id = 1, Email = "joe@email.com", FirstName = "Joe", LastName = "Joe", Password = "12345", RoleId = Role.Admin}
            };

            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void CreateRequest()
        {
            _request = new AddRecipeCommand { Name = "test-recipe-valid", Description = "test", IngredientIds = new List<int> { 1, 2 } };
        }
    }
}
