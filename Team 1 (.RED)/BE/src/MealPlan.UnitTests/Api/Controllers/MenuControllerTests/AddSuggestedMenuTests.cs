using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Meals;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Commands;
using MealPlan.Data.Models.Menus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.MenuControllerTests
{
    [TestFixture]
    public class AddSuggestedMenuTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private AddSuggestedMenuRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new MenuController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendAddSuggestedMenu()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddSuggestedMenuCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.AddSuggestedMenu(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddSuggestedMenuCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.AddSuggestedMenu(_request);

            result.Should().BeOfType<OkResult>();
        }

        private void CreateRequest()
        {
            _request = new AddSuggestedMenuRequest()
            {
                Name = "name",
                Description = "description",
                CategoryId = MenuCategory.Fitness,
                Meals = new List<AddSuggestedMealRequest> { }
            };
        }
    }
}