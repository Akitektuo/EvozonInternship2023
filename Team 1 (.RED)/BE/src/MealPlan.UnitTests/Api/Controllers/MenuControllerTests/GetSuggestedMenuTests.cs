using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Models;
using MealPlan.Business.Menus.Queries;
using MealPlan.Data.Models.Menus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.MenuControllerTests
{
    [TestFixture]
    public class GetSuggestedMenuTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private GetSuggestedMenuRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new MenuController(_mediator.Object);

            CreateRequest();
        }

        [Test]
        public async Task ShouldSendGetSuggestedMenuQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetSuggestedMenuQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new SuggestedMenu()));

            var result = await _controller.GetSuggestedMenu(_request);

            _mediator.Verify(m => m.Send(It.Is<GetSuggestedMenuQuery>(m => m.PriceSuggestion == 25 && m.CategoryId == MenuCategory.Vegetarian), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetSuggestedMenu(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetSuggestedMenuRequest
            {
                CategoryId = MenuCategory.Vegetarian,
                PriceSuggestion = 25
            };
        }
    }
}