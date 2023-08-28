using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Recipes;
using MealPlan.API.Requests.Shared;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.RecipeControllerTests
{
    [TestFixture]
    public class GetRecipesTests
    {
        private RecipeController _controller;
        private Mock<IMediator> _mediator;
        private GetRecipesRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new RecipeController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetRecipesQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetRecipesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GetRecipesModel()));

            var result = await _controller.GetRecipes(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetRecipesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetRecipes(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            var paginationModel = new PaginationModel
            {
                PageNumber = 1,
                PageSize = 10
            };
            _request = new GetRecipesRequest { PaginationModel = paginationModel };
        }
    }
}
