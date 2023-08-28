using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class ApproveOrderRequestValidatorTests
    {
        private ApproveOrderRequestValidator _validator;
        private IFixture _fixture;
        
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _validator = new ApproveOrderRequestValidator();
        }

        [Test]
        public void ShouldBeValid()
        {
            var model = _fixture.Build<ApproveOrderRequest>()
                .With(x => x.OrderID, 1).Create();
            
            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenOrderIdIsNull_ShouldReturnError()
        {
            var model = _fixture.Build<ApproveOrderRequest>()
                .Without(x => x.OrderID).Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.OrderID);
        }

        [Test]
        public void WhenOrderIdIsLessThan0_ShouldReturnError()
        {
            var model = _fixture.Build<ApproveOrderRequest>()
                .With(x => x.OrderID, -10).Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.OrderID);
        }
    }
}