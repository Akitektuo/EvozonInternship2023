using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.API.Requests.Shared;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
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

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetVersionQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllMenusQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetAllMenusModel()));

            var result = await _controller.GetAllMenus(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetAllMenusQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetAllMenus(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            PaginationModel paginationModel = new PaginationModel
            {
                PageNumber = 2,
                PageSize = 3,

            };
            _request = new GetAllMenusRequest { PaginationModel = paginationModel };
        }
    }
}
