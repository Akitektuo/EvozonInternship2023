using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data.Models.Meals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.MenuControllerTests
{

    public class GetGeneratedMenuTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private GetGeneratedMenuRequest _request;

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
        public async Task ShouldSendGetGeneratedMenuQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetGeneratedMenuQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GeneratedMenuModel()));

            var result = await _controller.GenerateMenuSuggestion(_request);

            _mediator.Verify(m => m.Send(It.Is<GetGeneratedMenuQuery>(g => g.MenuType == MenuType.FoodLover && g.PriceSuggestion == 40), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GenerateMenuSuggestion(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetGeneratedMenuRequest { MenuType = MenuType.FoodLover, PriceSuggestion = 40 };
        }
    }
}
