using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class DenyOrderRequestValidatorTests
    {
        private IFixture _fixture;
        private DenyOrderRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _validator = new DenyOrderRequestValidator();
        }

        [TearDown]
        public void TearDown()
        {
            _fixture = null;
            _validator = null;
        }

        [Test]
        public void ShouldBeValid()
        {
            var model = _fixture.Build<DenyOrderRequest>()
                .With(x => x.OrderID, 1)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenOrderIDIsNull_ShouldReturnError()
        {
            var model = _fixture.Build<DenyOrderRequest>()
                .Without(x => x.OrderID)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.OrderID);
        }

        [Test]
        public void WhenOrderIDIsLessThan1_ShouldReturnError()
        {
            var model = _fixture.Build<DenyOrderRequest>()
                .With(x => x.OrderID, -10)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.OrderID);
        }
    }
}