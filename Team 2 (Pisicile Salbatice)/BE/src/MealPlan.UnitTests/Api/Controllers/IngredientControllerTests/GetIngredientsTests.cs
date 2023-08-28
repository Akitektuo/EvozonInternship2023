using FluentAssertions;
using MealPlan.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Business.Ingredients.Models;

namespace MealPlan.UnitTests.Api.Controllers.IngredientControllerTests
{
    [TestFixture]
    public class GetIngredientsTests
    {
        private IngredientController _controller;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new IngredientController(_mediator.Object);
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetIngredientsQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetIngredientsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetIngredientsModel()));

            var result = await _controller.GetIngredients();

            _mediator.Verify(m => m.Send(It.IsAny<GetIngredientsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetIngredients();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
