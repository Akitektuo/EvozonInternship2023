using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Menus;
using MealPlan.Data.Models.Menus;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class GetAllMenusRequestValidatorTests
    {
        private GetAllMenusRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetAllMenusRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void ShouldBeValid()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
                .With(x => x.PageNumber, 1)
                .With(x => x.PageSize, 3)
                .With(x => x.CategoryId, MenuCategory.FoodLover)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenPageNumberNotGreaterThan0_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
               .With(x => x.PageNumber, -1)
               .With(x => x.PageSize, 3)
               .With(x => x.CategoryId, MenuCategory.FoodLover)
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Test]
        public void WhenPageSizeNotGreaterThan0_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
               .With(x => x.PageNumber, 1)
               .With(x => x.PageSize, -3)
               .With(x => x.CategoryId, MenuCategory.FoodLover)
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Test]
        public void WhenPageNumberAndPageSizeNotGreaterThan0_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
               .With(x => x.PageNumber, -1)
               .With(x => x.PageSize, -3)
               .With(x => x.CategoryId, MenuCategory.FoodLover)
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PageNumber);
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Test]
        public void WhenCategoryIsNotInEnum_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
               .With(x => x.PageNumber, 1)
               .With(x => x.PageSize, 3)
               .With(x => x.CategoryId, (MenuCategory) 7)
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Test]
        public void WhenCategoryIsEmpty_ShouldReturnError()
        {
            var model = _fixture.Build<GetAllMenusRequest>()
               .With(x => x.PageNumber, 1)
               .With(x => x.PageSize, 3)
               .Without(x => x.CategoryId)
               .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}