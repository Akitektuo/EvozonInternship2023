using FluentAssertions;
using MealPlan.Business.Recipes.Handlers;
using MealPlan.Business.Recipes.Models;
using MealPlan.Business.Recipes.Queries;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Recipes;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Recipes.Handlers
{
    [TestFixture]
    public class GetUnusedRecipesQueryHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private GetUnusedRecipesQueryHandler _handler;
        private GetUnusedRecipesQuery _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new GetUnusedRecipesQueryHandler(_context.Object);

            CreateRequest();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldReturnUnusedRecipeOnly()
        {
            var result = await _handler.Handle(_request, new CancellationToken());

            var recipesOverview = new List<RecipeOverview>
            {
                new RecipeOverview { Id = 4, Name = "Recipe for Cea mai buna pizza", Description = "Cea mai buna pizza"}
            };

            result.TotalRecords.Should().Be(1);
            result.Items.Should().BeEquivalentTo(recipesOverview);
        }

        private void SetupContext()
        {
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = 1,
                    Description = "Cel mai bun tort cu ciocolata si capsuni",
                    Name = "Recipe for Tort cu ciocolata",
                    Meal = new Meal
                    {
                        Name = "Tort cu ciocolata",
                        Price = 75,
                        MealTypeId = MealType.Dessert
                    }
                },
                new Recipe
                {
                    Id = 2,
                    Description = "Cele mai bune paste cu pui si ciuperci",
                    Name = "Recipe for Paste cu pui si ciuperci",
                    Meal = new Meal
                    {
                        Name = "Paste cu pui si ciuperci",
                        Price = 40,
                        MealTypeId = MealType.Lunch
                    }
                },
                new Recipe
                {
                    Id = 3,
                    Description = "Cei mai buni papanasi din Ro",
                    Name = "Recipe for Cei mai buni papanasi din Ro",
                    Meal = new Meal
                    {
                        Name = "Papanasi fierti",
                        Price = 42,
                        MealTypeId = MealType.Dessert
                    }
                },
                new Recipe
                {
                    Id = 4,
                    Description = "Cea mai buna pizza",
                    Name = "Recipe for Cea mai buna pizza",
                }
            };

            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
        }

        private void CreateRequest()
        {
            _request = new GetUnusedRecipesQuery { PageNumber = 1, PageSize = 2 };
        }
    }
}