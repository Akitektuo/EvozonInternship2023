using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Recipes;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Business.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.RecipeControllerTests
{
    [TestFixture]
    public class GetUnusedRecipesTests
    {
        private RecipeController _controller;
        private Mock<IMediator> _mediator;
        private GetUnusedRecipesRequest _request;

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
        public async Task ShouldSendGetUnusedRecipesQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetUnusedRecipesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PaginationModel<RecipeOverview>
                {
                    Items = new List<RecipeOverview>
                    {
                        new RecipeOverview { Id = 1, Name = "Tort cu ciocolata", Description = "Cel mai bun tort cu ciocolata si capsuni" },
                        new RecipeOverview { Id = 2, Name = "Paste cu pui si ciuperci", Description = "Cele mai bune paste cu pui si ciuperci" }
                    },
                    TotalRecords = 3
                }));

            var result = await _controller.GetAllRecipes(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetUnusedRecipesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetAllRecipes(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetUnusedRecipesRequest { PageNumber = 2, PageSize = 3 };
        }
    }
}