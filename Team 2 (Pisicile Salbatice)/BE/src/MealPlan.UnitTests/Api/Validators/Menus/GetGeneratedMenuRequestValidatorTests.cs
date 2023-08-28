using AutoFixture;
using MealPlan.API.Requests.Menus;
using MealPlan.Data.Models.Meals;
using NUnit.Framework;
using FluentValidation.TestHelper;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    public class GetGeneratedMenuRequestValidatorTests
    {
        private GetGeneratedMenuRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetGeneratedMenuRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenGoodData_ShoulNotReturnError() 
        {
            var model = _fixture.Build<GetGeneratedMenuRequest>()
                .With(x => x.MenuType, MenuType.FoodLover)
                .With(x => x.PriceSuggestion, 10)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenEmptyMenuType_ShouldReturnError()
        {
            var model = _fixture.Build<GetGeneratedMenuRequest>()
                .Without(x => x.MenuType)
                .With(x => x.PriceSuggestion, 10)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.MenuType);
        }

        [Test]
        public void WhenEmptyPriceSuggestion_ShouldReturnError()
        {
            var model = _fixture.Build<GetGeneratedMenuRequest>()
                .Without(x => x.PriceSuggestion)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PriceSuggestion);
        }

        [Test]
        public void WhenInvalidMenuType_ShouldReturnError()
        {
            var model = _fixture.Build<GetGeneratedMenuRequest>()
                .With(x => x.MenuType, (MenuType)101)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.MenuType);
        }

        [Test]
        public void WhenInvalidPriceSuggestion_ShouldReturnError()
        {
            var model = _fixture.Build<GetGeneratedMenuRequest>()
                .With(x => x.PriceSuggestion, -123)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PriceSuggestion);
        }
    }
}
