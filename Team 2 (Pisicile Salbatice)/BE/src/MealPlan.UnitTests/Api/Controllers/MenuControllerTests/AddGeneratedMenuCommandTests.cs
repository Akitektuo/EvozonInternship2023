using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Menus.AddGeneratedMenu;
using MealPlan.Business.Menus.Commands.AddGeneratedMenu;
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
    public class AddGeneratedMenuCommandTests
    {
        private MenuController _controller;
        private Mock<IMediator> _mediator;
        private AddGeneratedMenuRequest _request;

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
        public async Task WhenValidRequest_ReturnOk()
        {
            var result = await _controller.AddGeneratedMenu(_request);

            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task ShouldSendOkOnce()
        {
            _mediator.Setup(m => m.Send(It.IsAny<AddGeneratedMenuCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.AddGeneratedMenu(_request);

            _mediator.Verify(m => m.Send(It.IsAny<AddGeneratedMenuCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private void CreateRequest()
        {
            _request = new AddGeneratedMenuRequest
            {
                Name = "M2",
                MenuTypeId = MenuType.FoodLover,
                Meals = new List<GeneratedMeal>()
                {
                    new GeneratedMeal{
                        Name = "M10",
                        Description = "d1",
                        Price = 20,
                        MealTypeId = MealType.Dinner,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMeal{
                        Name = "M12",
                        Description = "d2",
                        Price = 20,
                        MealTypeId = MealType.Lunch,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test2",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMeal{
                        Name = "M13",
                        Description = "d3",
                        Price = 20,
                        MealTypeId = MealType.Breakfast,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test3",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMeal{
                        Name = "M14",
                        Description = "d4",
                        Price = 20,
                        MealTypeId = MealType.Snack,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test4",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                    new GeneratedMeal{
                        Name = "M15",
                        Description = "d5",
                        Price = 20,
                        MealTypeId = MealType.Dessert,
                        Recipe = new GeneratedRecipe
                        {
                            Name = "test",
                            Description = "test",
                            Ingredients = new List<string>
                            {
                                "Oua",
                                "Mere",
                                "Pere"
                            }
                        }
                    },
                }
            };
        }
    }
}
