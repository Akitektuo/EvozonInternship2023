using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Meals;
using MealPlan.API.Requests.Orders;
using MealPlan.UnitTests.Utils;
using NUnit.Framework;
using System;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class AddOrderValidatorTests
    {
        private AddOrderRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _validator = new AddOrderRequestValidator();
        }

        [Test]
        public void WhenAddressMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.Address)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Test]
        public void WhenAddressShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.Address, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Address);
        }

        [Test]
        public void WhenAddressLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.Address, StringUtils.RandomString(71))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Address);
        }

        [Test]
        public void WhenPhoneNumberMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.PhoneNumber)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenPhoneNumberShorterThanMin_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.PhoneNumber, "a")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenPhoneNumberLongerThanMax_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.PhoneNumber, StringUtils.RandomString(51))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenPhoneNumberNotValid_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.PhoneNumber, "035433423A")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenStartDateMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.StartDate)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenStartDateLessThanTomorrow_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Today)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenStartDateGreaterThanNextMonth_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Today.AddDays(32))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        public void WhenEndDateMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.EndDate)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.EndDate);
        }

        [Test]
        public void WhenEndDateLessThanTomorrow_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.EndDate, DateTime.Today)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.EndDate);
        }

        [Test]
        public void WhenEndDateGreaterThanNextMonth_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.EndDate, DateTime.Today.AddDays(32))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.EndDate);
        }

        [Test]
        public void WhenStartDateGreaterThanEndDate_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Today.AddDays(2))
                .With(x => x.EndDate, DateTime.Today.AddDays(1))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor("Dates");
        }

        [Test]
        public void WhenMenuIdMissing_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.MenuId)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }

        [Test]
        public void WhenMenuIdNegative_ShouldReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.MenuId, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }
    }
}