using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.RecipeControllerTests
{
    [TestFixture]
    public class GetRecipeTests
    {
        private RecipeController _controller;
        private Mock<IMediator> _mediator;
        private GetRecipeRequest _request;

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
        public async Task ShouldSendGetRecipeQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRecipeQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new RecipeModel()));

            var result = await _controller.GetRecipe(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetRecipeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetRecipe(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetRecipeRequest { RecipeId = 1 };
        }
    }

}
