using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;

namespace MealPlan.UnitTests.Api.Controllers.MenuControllerTests
{
    [TestFixture]
    public class GetMenuTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private GetMenuRequest _request;

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
        public async Task ShouldSendGetMenuQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetMenuDetailsModel()));

            var result = await _controller.GetMenu(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetMenu(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetMenuRequest { MenuId = 1 };
        }
    }
}
