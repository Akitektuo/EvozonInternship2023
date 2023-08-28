using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    public class GetMenuRequestValidatorTests
    {
        private GetMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetMenuRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShouldNotReturnError()
        {
            var model = _fixture.Build<GetMenuRequest>()
               .With(x => x.MenuId, 2)
               .Create();
            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenMissingMenuId_ShouldReturnError()
        {
            var model = _fixture.Build<GetMenuRequest>()
               .Without(x => x.MenuId)
               .Create();
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }

        [Test]
        public void WhenInvalidMenuId_ShouldReturnError()
        {
            var model = _fixture.Build<GetMenuRequest>()
               .With(x => x.MenuId, -1)
               .Create();
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.MenuId);
        }
    }
}
