using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Business.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.MenuControllerTests
{
    [TestFixture]
    public class GetAllMenusTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private GetAllMenusRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new MenuController(_mediator.Object);

            CreateRequest();
        }

        [Test]
        public async Task ShouldSendGetAllMenusQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllMenusQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PaginationModel<MenuOverview>()));

            var result = await _controller.GetAllMenus(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetAllMenusQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetAllMenus(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        private void CreateRequest()
        {
            _request = new GetAllMenusRequest
            {
                PageNumber = 1,
                PageSize = 2
            };
        }
    }
}