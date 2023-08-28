using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus;
using MealPlan.Business.Menus.Commands;
using MealPlan.Data.Models.Meals;
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
    public class AddMenuCommandTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private AddMenuRequest _request;

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
        public async Task WhenValidRequest_ReturnCreatedAtAction()
        {
            var result = await _controller.AddMenu(_request);

            result.Result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task ShouldSendGetVersionQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddMenuCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.AddMenu(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddMenuCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private void CreateRequest()
        {
            _request = new AddMenuRequest
            {
                MenuName = "M2",
                MenuTypeId = MenuType.Fitness,
                Meals = new List<MealReceived>()
                {
                    new MealReceived{
                        Name = "M10",
                        Description = "d1",
                        Price = 20,
                        MealTypeId = MealType.Breakfast,
                        RecipeId = 1
                    },
                    new MealReceived{
                        Name = "M12",
                        Description = "d2",
                        Price = 20,
                        MealTypeId = MealType.Lunch,
                        RecipeId = 2
                    },
                    new MealReceived{
                        Name = "M13",
                        Description = "d3",
                        Price = 20,
                        MealTypeId = MealType.Dinner,
                        RecipeId = 3
                    },
                    new MealReceived{
                        Name = "M14",
                        Description = "d4",
                        Price = 20,
                        MealTypeId = MealType.Dessert,
                        RecipeId = 4
                    },
                    new MealReceived{
                        Name = "M15",
                        Description = "d5",
                        Price = 20,
                        MealTypeId = MealType.Snack,
                        RecipeId = 5
                    },
                }
            };
        }
    }
}
