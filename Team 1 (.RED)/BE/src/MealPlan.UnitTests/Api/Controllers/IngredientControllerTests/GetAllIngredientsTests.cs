using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.Business.Ingredients.Models;
using MealPlan.Business.Ingredients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.IngredientControllerTests
{
    [TestFixture]
    public class GetAllIngredientsTests
    {
        private IngredientController _controller;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();

            _controller = new IngredientController(_mediator.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mediator = null;

            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetAllIngredients()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllIngredientsQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new List<IngredientModel>()));

            var result = await _controller.GetAllIngredients();

            _mediator.Verify(m => m.Send(It.IsAny<GetAllIngredientsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetAllIngredients();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}