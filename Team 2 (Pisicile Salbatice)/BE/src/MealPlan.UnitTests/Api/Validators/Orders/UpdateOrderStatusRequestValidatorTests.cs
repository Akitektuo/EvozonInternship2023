using AutoFixture;
using NUnit.Framework;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using MealPlan.Data.Models.Orders;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class UpdateOrderStatusRequestValidatorTests
    {
        private UpdateOrderStatusRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new UpdateOrderStatusRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<UpdateOrderStatusRequest>()
                .With(o => o.StatusId, Status.Approved)
                .With(o => o.OrderId, 1)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenStatusIdNotInEnum_ShouldThrowError()
        {
            var model = _fixture.Build<UpdateOrderStatusRequest>()
                .With(o => o.StatusId, (Status)0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StatusId);
        }

        [Test]
        public void WhenStatusIdMissing_ShouldThrowError()
        {
            var model = _fixture.Build<UpdateOrderStatusRequest>()
                .Without(o => o.StatusId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StatusId);
        }

        [Test]
        public void WhenOrderIdLessThan1_ShouldThrowError()
        {
            var model = _fixture.Build<UpdateOrderStatusRequest>()
                .With(o => o.OrderId, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.OrderId);
        }

        [Test]
        public void WhenOrderIdMissing_ShouldThrowError()
        {
            var model = _fixture.Build<UpdateOrderStatusRequest>()
                .Without(o => o.OrderId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.OrderId);
        }
    }
}
