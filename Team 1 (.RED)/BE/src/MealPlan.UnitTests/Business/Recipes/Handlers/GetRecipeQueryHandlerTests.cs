using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Handlers;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Recipes.Handlers
{
    [TestFixture]
    public class GetRecipeQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetRecipeQueryHandler _handler;
        private GetRecipeQuery _request;
        private Recipe _recipe;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetRecipeQueryHandler(_context.Object);

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
         
            result.Description.Should().Be(_recipe.Description);
            result.Name.Should().Be(_recipe.Name);
            result.Ingredients[0].Should().Be(_recipe.Ingredients[0].Name);
        }


        [Test]
        public async Task WhenUsingInvalidId_ShouldThrowError()
        {
            _request.Id = 256;

            Func<Task> action = async () => await _handler.Handle(_request, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException 
                    { 
                        Code = (int)ErrorCode.RecipeNotFound, 
                        Message = "Recipe not found"
                    }));
        }

        private void SetupContext()
        {
            _recipe = new Recipe
            {
                Id = 1,
                Description = "First recipe description",
                Name = "Recipe for first meal",
                Meal = new Meal { Id = 1, Name = "First meal" },
                Ingredients = new List<Ingredient> { new Ingredient { Name = "first ingredient" } }
            };

            var recipes = new List<Recipe> 
            { 
                _recipe,
                new Recipe { Id = 2},
                new Recipe { Id = 3}
            };

            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
        }

        private void CreateRequest()
        {
            _request = new GetRecipeQuery { Id = 1 };
        }
    }
}