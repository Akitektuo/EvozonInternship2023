using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Shared;
using NUnit.Framework;

namespace MealPlan.UnitTests.Api.Validators.Menus
{
    [TestFixture]
    public class PaginationModelValidatorTests
    {
        private PaginationModelValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new PaginationModelValidator();
            _fixture = new Fixture();
        }
        [Test]
        public void WhenGoodInput_ShouldNotReturnError()
        {
            var model = _fixture.Build<PaginationModel>()
                .With(x => x.PageNumber, 3)
                .With(x => x.PageSize, 2)
                .Create();

            _validator.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void WhenNegativePageNumber_ShouldReturnError()
        {
            var model = _fixture.Build<PaginationModel>()
                .With(x => x.PageNumber, -1)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageNumber);
        }

        [Test]
        public void WhenNegativePageSize_ShouldReturnError()
        {
            var model = _fixture.Build<PaginationModel>()
                .With(x => x.PageSize, -3)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageSize);
        }

        [Test]
        public void WhenPageNumberZero_ShouldReturnError()
        {
            var model = _fixture.Build<PaginationModel>()
                .With(x => x.PageNumber, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageNumber);
        }

        [Test]
        public void WhenPageSizeZero_ShouldReturnError()
        {
            var model = _fixture.Build<PaginationModel>()
                .With(x => x.PageSize, 0)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.PageSize);
        }
    }
}
