using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Recipes.Commands;
using MealPlan.Business.Recipes.Handlers;
using MealPlan.Data;
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
    public class AddRecipeCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddRecipeCommandHandler _handler;
        private AddRecipeCommand _successfulRequest;
        private AddRecipeCommand _failedIngredientsRequest;
        private AddRecipeCommand _failedEmptyIngredientsRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddRecipeCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldAddRecipe()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenNotAllIngredientsExist_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_failedIngredientsRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Some ingredients from the list were not found",
                        Code = (int)ErrorCode.IngredientNotFound
                    }));
        }

        [Test]
        public async Task WhenNotAllIngredientsExist_ShouldNotCallSaveChanges()
        {
            _context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            try
            {
                var result = await _handler.Handle(_failedIngredientsRequest, new CancellationToken());
            }
            catch (CustomApplicationException _)
            {
                _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            }
        }

        [Test]
        public async Task WhenIngredientsListEmpty_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_failedEmptyIngredientsRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Ingredients list is empty",
                        Code = (int)ErrorCode.EmptyIngredientsList
                    }));
        }

        [Test]
        public async Task WhenRoleAndIngredientsOk_ShouldCallSaveChanges()
        {
            _context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private void CreateCommands()
        {
            _successfulRequest = new AddRecipeCommand
            {
                Name = "Name1",
                Description = "TestAdd1",
                IngredientIds = new List<int> { 1, 2, 3 }
            };

            _failedIngredientsRequest = new AddRecipeCommand
            {
                Name = "Name2",
                Description = "TestAdd2",
                IngredientIds = new List<int> { 1, 2, 909 }
            };

            _failedEmptyIngredientsRequest = new AddRecipeCommand
            {
                Name = "Name3",
                Description = "TestAdd3",
                IngredientIds = new List<int> {}
            };
        }

        private void SetupContext()
        {
            var ingredients = new List<Ingredient>()
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "Name1"
                },
                new Ingredient
                {
                    Id= 2,
                    Name = "Name2"
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Name3"
                }
            };

            var recipes = new List<Recipe> { };
            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
        }
    }
}