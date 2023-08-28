using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Shared;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class OrderByModelValidatorTests
    {
        private OrderByModelValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new OrderByModelValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<OrderByModel>()
                .With(x => x.Column, "UserEmail")
                .With(x => x.SortOrder, "asc")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenSortOrderIsInvalid_ShouldThrowError()
        {
            var model = _fixture.Build<OrderByModel>()
                .With(x => x.SortOrder, "abc")
                .With(x => x.Column, "UserEmail")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.SortOrder);
        }
    }
}
