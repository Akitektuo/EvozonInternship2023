using AutoFixture;
using MealPlan.API.Requests.Versions;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System.Linq;

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
