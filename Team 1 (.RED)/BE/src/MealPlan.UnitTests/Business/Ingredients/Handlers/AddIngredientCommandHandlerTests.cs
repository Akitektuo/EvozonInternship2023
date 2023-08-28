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
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Ingredients.Handlers
{
    [TestFixture]
    public class AddIngredientCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddIngredientCommandHandler _handler;
        private AddIngredientCommand _successfulRequest;
        private AddIngredientCommand _ingredientAlreadyUsedRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddIngredientCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [Test]
        public async Task ShouldAddIngredient()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenIngredientAlreadyExist_ShouldThrowCustomApplicationException()
        {
            Func<Task> action = async () => await _handler.Handle(_ingredientAlreadyUsedRequest, new CancellationToken());

            await action.Should()
                .ThrowAsync<CustomApplicationException>()
                .WithMessage(JsonSerializer.Serialize(
                    new CustomException
                    {
                        Message = "Ingredient already exist",
                        Code = (int)ErrorCode.IngredientAlreadyExist
                    }));
        }

        private void CreateCommands()
        {
            _successfulRequest = new AddIngredientCommand
            {
                Name = "Piper"
            };

            _ingredientAlreadyUsedRequest = new AddIngredientCommand
            {
                Name = "Cascaval"
            };
        }

        private void SetupContext()
        {
            var ingredients = new List<Ingredient>
            {   new Ingredient
                {
                    Name = "Oua"
                },
                new Ingredient
                {
                    Name = "Cascaval"
                },
                new Ingredient
                {
                   Name = "Sunca"
                },
                new Ingredient
                {
                    Name = "Sare"
                },
                new Ingredient
                {
                    Name = "ton"
                }
            };

            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
        }
    }
}