using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using MealPlan.API.Requests.Shared;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class GetOrderRequestValidatorTests
    {
        private GetOrdersRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetOrdersRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<GetOrdersRequest>()
                .With(x => x.Pagination, new PaginationModel())
                .With(x => x.Filtration, new FiltrationModel())
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }
    }
}
