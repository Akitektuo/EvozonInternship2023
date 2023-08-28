using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using MealPlan.Data.Models.Orders;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class GetAllOrdersRequestValidatorTests
    {
        private GetAllOrdersRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _validator = new GetAllOrdersRequestValidator();
        }

        [Test]
        public void WhenSearchTextGreaterThanMaximumLength_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllOrdersRequest>()
                .With(x => x.FilteringModel, _fixture.Build<FilteringModel>()
                .With(x => x.SearchText, StringUtils.RandomString(51)).Create())
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FilteringModel.SearchText);
        }

        [Test]
        public void WhenOrderByColumnsGreaterThanMaximumLength_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllOrdersRequest>()
               .With(x => x.OrderingModel, _fixture.Build<OrderingModel>()
               .With(x => x.OrderByColumns, StringUtils.RandomString(101)).Create())
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.OrderingModel.OrderByColumns);
        }

        [Test]
        public void WhenFiltreByStatusIsNotInEnum_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllOrdersRequest>()
                .With(x => x.FilteringModel, _fixture.Build<FilteringModel>()
                .With(x => x.FilterByStatus, (OrderStatus)7).Create())
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.FilteringModel.FilterByStatus);
        }
    }
}