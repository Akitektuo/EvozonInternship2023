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

        [Test]
        public async Task ShouldSendAddIngredient()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddIngredientCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.AddIngredient(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddIngredientCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.AddIngredient(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new AddIngredientRequest
            {
                Name = "Rosii"
            };
        }
    }
}