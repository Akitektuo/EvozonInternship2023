using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Orders;
using NUnit.Framework;
using System;

namespace MealPlan.UnitTests.Api.Validators.Orders
{
    [TestFixture]
    public class AddOrderRequestValidatorTests
    {
        private AddOrderRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new AddOrderRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.MenuId, 1)
                .With(x => x.PhoneNumber, "0770 121 194")
                .With(x => x.ShippingAddress, "Motilor Street")
                .With(x => x.StartDate, DateTime.Now.AddDays(1))
                .With(x => x.EndDate, DateTime.Now.AddDays(4))
                .With(x => x.Email, "jon@snow.com")
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNoMenuId_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.MenuId)
                .With(x => x.PhoneNumber, "0770 121 194")
                .With(x => x.ShippingAddress, "Motilor Street")
                .With(x => x.StartDate, DateTime.Now.AddDays(1))
                .With(x => x.EndDate, DateTime.Now.AddDays(4))
                .With(x => x.Email, "jon@snow.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }

        [Test]
        public void WhenInvalidMenuId_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.MenuId, -11)
                .With(x => x.PhoneNumber, "0770 121 194")
                .With(x => x.ShippingAddress, "Motilor Street")
                .With(x => x.StartDate, DateTime.Now.AddDays(1))
                .With(x => x.EndDate, DateTime.Now.AddDays(4))
                .With(x => x.Email, "jon@snow.com")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }

        [Test]
        public void WhenNoPhoneNumber_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.PhoneNumber)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenTooLongPhoneNumber_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.PhoneNumber, "0770 121 19409437492378423742093784092378409723974092374902374092370497023478")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PhoneNumber);
        }

        [Test]
        public void WhenNoShippingAddress_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.ShippingAddress)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ShippingAddress);
        }

        [Test]
        public void WhenTooLongShippingAddress_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.ShippingAddress, "asdasdasjhdasjhdasjkghdasgjkdjghajksdgaskjghdjkasgdjkasgdjkasgjdkgaskghd asdvgaksjhdgkasjgdjkasgdjkhagsjhkdgasjhdgj kasgdkasgdaksg sakdasasdjbkasdasdasdasdasdasdasdasdadasdqwedfvwefqdq")
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.ShippingAddress);
        }

        [Test]
        public void WhenNoStartDate_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.StartDate)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenStartDateIsToday_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Now)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenStartDateIsInPast_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Now.AddDays(-10))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenStartDate30DaysLaterThanPresentDay_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Now.AddDays(30))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenNoEndDate_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .Without(x => x.EndDate)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.StartDate);
        }

        [Test]
        public void WhenEndDateIsBeforeStartDate_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.StartDate, DateTime.Now.AddDays(5))
                .With(x => x.EndDate, DateTime.Now.AddDays(3))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.EndDate);
        }

        public void WhenEndDate30DaysLaterThanPresentDay_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.EndDate, DateTime.Now.AddDays(30))
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.EndDate);
        }

        [Test]
        public void WhenPeriodIntervalLargerThan30_ShouldThrowError()
        {
            var model = _fixture.Build<AddOrderRequest>()
                .With(x => x.MenuId, 1)
                .With(x => x.PhoneNumber, "0770 121 194")
                .With(x => x.ShippingAddress, "Motilor Street")
                .With(x => x.StartDate, DateTime.Now.AddDays(1))
                .With(x => x.EndDate, DateTime.Now.AddDays(40))
                .Create();

            _validator.TestValidate(model).ShouldHaveAnyValidationError();
        }
    }
}
