using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Ingredients;
using MealPlan.Business.Ingredients.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.IngredientControllerTests
{
    [TestFixture]
    public class AddIngredientTests
    {
        private IngredientController _controller;
        private Mock<IMediator> _mediator;
        private AddIngredientRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _controller = new IngredientController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task WhenValidRequest_ReturnOkResult()
        {
            var result = await _controller.AddIngredient(_request);

            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task ShouldSendRequestOnce()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddIngredientCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            await _controller.AddIngredient(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddIngredientCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private void CreateRequest()
        {
            _request = new AddIngredientRequest { IngredientName = "ingredient" };
        }
    }
}
