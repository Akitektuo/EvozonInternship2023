using AutoFixture;
using FluentValidation.TestHelper;
using MealPlan.API.Requests.Versions;
using NUnit.Framework;
using System;

namespace MealPlan.UnitTests.Api.Validators.Versions
{
    [TestFixture]
    public class GetVersionRequestValidatorTests
    {
        private GetVersionRequestValidator _validator;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _validator = new GetVersionRequestValidator();
            _fixture = new Fixture();
        }

        [Test]
        public void WhenNameMissing_ShouldReturnError()
        {
            var model = _fixture.Build<GetVersionRequest>()
                .Without(x => x.Name)
                .Create();

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }
    }
}