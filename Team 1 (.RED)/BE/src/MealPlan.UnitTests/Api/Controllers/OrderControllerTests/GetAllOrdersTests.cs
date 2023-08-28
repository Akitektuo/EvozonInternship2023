using FluentAssertions;
using MealPlan.API.Controllers;
using MealPlan.API.Requests.Orders;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Orders.Queries;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Api.Controllers.OrderControllerTests
{
    [TestFixture]
    public class GetAllOrdersTests
    {
        private OrderController _controller;
        private Mock<IMediator> _mediator;
        private GetAllOrdersRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new OrderController(_mediator.Object);

            CreateRequest();
        }

        [Test]
        public async Task ShouldSendGetAllOrdersQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PaginationModel<OrderDetails>
                {
                    Items = new List<OrderDetails>
                    {
                        new OrderDetails
                        {
                            UserEmail = "paulac@yahoo.com",
                            MenuName = "Meniul zilei",
                            StartDate = new DateTime(2023, 9, 17),
                            EndDate = new DateTime(2023, 10, 17),
                            OrderStatus = OrderStatus.Pending.ToString(),
                        },
                         new OrderDetails
                        {
                            UserEmail = "oanat@yahoo.com",
                            MenuName = "Desert",
                            StartDate = new DateTime(2023, 9, 18),
                            EndDate = new DateTime(2023, 10, 18),
                            OrderStatus = OrderStatus.Pending.ToString(),
                        },
                    },
                    TotalRecords = 2
                }));

            var result = await _controller.GetAllOrders(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetAllOrders(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetAllOrdersRequest { PageNumber = 2, PageSize = 2, FilteringModel = { FilterByStatus = OrderStatus.Denied } };
        }
    }
}