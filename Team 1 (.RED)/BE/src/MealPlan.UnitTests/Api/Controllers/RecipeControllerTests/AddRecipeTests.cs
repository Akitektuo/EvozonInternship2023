using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Recipes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.RecipeControllerTests
{
    [TestFixture]
    public class AddRecipeTests
    {
        private RecipeController _controller;
        private Mock<IMediator> _mediator;
        private AddRecipeRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new RecipeController(_mediator.Object);

            CreateRequest();
            
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendAddRecipe()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddRecipeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.AddRecipe(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddRecipeCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.AddRecipe(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new AddRecipeRequest
            {
                Name = "Name1",
                Description = "ControllerTestAdd1",
                IngredientIds = new List<int> { 1, 2, 3 }
            };
        }
    }
}