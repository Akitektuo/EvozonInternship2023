using FluentAssertions;
using MealPlan.Business.Exceptions;
using MealPlan.Business.Meals.Commands;
using MealPlan.Business.Menus.Commands;
using MealPlan.Business.Menus.Handlers;
using MealPlan.Business.Recipes.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Recipes;
using MealPlan.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.UnitTests.Business.Menus.Handlers
{
    public class AddSuggestedMenuCommandHandlerTests
    {
        private Mock<MealPlanContext> _context;
        private AddSuggestedMenuCommandHandler _handler;
        private AddSuggestedMenuCommand _successfulRequest;

        [SetUp]
        public void Init()
        {
            _context = new Mock<MealPlanContext>();
            _handler = new AddSuggestedMenuCommandHandler(_context.Object);

            CreateCommands();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldAddSuggestedMenu()
        {
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _handler.Handle(_successfulRequest, new CancellationToken());

            result.Should().BeTrue();
        }

        [Test]
        public async Task WhenIngredientsAlreadyInDatabase_ShouldNotDuplicateThem()
        {
            List<Ingredient> newIngredients = new List<Ingredient>();

            (await _handler.ChooseProperIngredientAsync("databaseIngredient1", newIngredients))
                .Should()
                .BeEquivalentTo(await _context.Object.Ingredients
                    .Where(i => i.Name.Equals("databaseIngredient1"))
                    .SingleOrDefaultAsync());

            (await _handler.ChooseProperIngredientAsync("databaseIngredient2", newIngredients))
                .Should()
                .BeEquivalentTo(await _context.Object.Ingredients
                    .Where(i => i.Name.Equals("databaseIngredient2"))
                    .SingleOrDefaultAsync());

            (await _handler.ChooseProperIngredientAsync("databaseIngredient3", newIngredients))
                .Should()
                .BeEquivalentTo(await _context.Object.Ingredients
                    .Where(i => i.Name.Equals("databaseIngredient3"))
                    .SingleOrDefaultAsync());

            (await _handler.ChooseProperIngredientAsync("databaseIngredient4", newIngredients))
                .Should()
                .BeEquivalentTo(await _context.Object.Ingredients
                    .Where(i => i.Name.Equals("databaseIngredient4"))
                    .SingleOrDefaultAsync());

            (await _handler.ChooseProperIngredientAsync("databaseIngredient5", newIngredients))
                .Should()
                .BeEquivalentTo(await _context.Object.Ingredients
                    .Where(i => i.Name.Equals("databaseIngredient5"))
                    .SingleOrDefaultAsync());
        }

        [Test]
        public async Task WhenIngredientsAlreadyAddedInCurrentCommand_ShouldNotDuplicateThem()
        {
            List<Ingredient> newIngredients = new List<Ingredient>();

            await _handler.ChooseProperIngredientAsync("ingredient1", newIngredients);
            await _handler.ChooseProperIngredientAsync("ingredient2", newIngredients);
            await _handler.ChooseProperIngredientAsync("ingredient1", newIngredients);
            await _handler.ChooseProperIngredientAsync("ingredient1", newIngredients);
            await _handler.ChooseProperIngredientAsync("ingredient1", newIngredients);

            newIngredients.Select(i => i.Name).Should().OnlyHaveUniqueItems();
        }

        private void CreateCommands()
        {
            var validMealList = new List<AddSuggestedMealCommand>
            {
                new AddSuggestedMealCommand
                {
                    Name = "Meal1",
                    Price = 1,
                    MealTypeId = MealType.Breakfast,
                    Recipe = new AddSuggestedRecipeCommand
                    {
                        Description = "RecipeDescription1",
                        Name = "RecipeName1",
                        Ingredients = new List<string> {"eggs", "tomatoes"}
                    }
                },
                new AddSuggestedMealCommand
                {
                    Name = "Meal2",
                    Price = 1,
                    MealTypeId = MealType.Lunch,
                    Recipe = new AddSuggestedRecipeCommand
                    {
                        Description = "RecipeDescription2",
                        Name = "RecipeName2",
                        Ingredients = new List<string> {"cheese"}
                    }
                },
                new AddSuggestedMealCommand
                {
                    Name = "Meal3",
                    Price = 1,
                    MealTypeId = MealType.Dinner,
                    Recipe = new AddSuggestedRecipeCommand
                    {
                        Description = "RecipeDescription3",
                        Name = "RecipeName3",
                        Ingredients = new List<string> {"test3"}
                    }
                },
                new AddSuggestedMealCommand
                {
                    Name = "Meal4",
                    Price = 1,
                    MealTypeId = MealType.Dessert,
                    Recipe = new AddSuggestedRecipeCommand
                    {
                        Description = "RecipeDescription4",
                        Name = "RecipeName4",
                        Ingredients = new List<string> {"test4"}
                    }
                },
                new AddSuggestedMealCommand
                {
                    Name = "Meal5",
                    Price = 1,
                    MealTypeId = MealType.Snack,
                    Recipe = new AddSuggestedRecipeCommand
                    {
                        Description = "RecipeDescription5",
                        Name = "RecipeName5",
                        Ingredients = new List<string> {"test5"}
                    }
                }
            };

            _successfulRequest = new AddSuggestedMenuCommand
            {
                Name = "Name1",
                Description = "Description1",
                CategoryId = MenuCategory.FoodLover,
                Meals = validMealList
            };
        }

        private void SetupContext()
        {
            var menus = new List<Menu> { };
            var recipes = new List<Recipe> { };
            var meals = new List<Meal> { };

            var ingredients = new List<Ingredient> 
            {
                new Ingredient
                {
                    Id = 1, 
                    Name = "databaseIngredient1"
                },

                new Ingredient
                {
                    Id = 2,
                    Name = "databaseIngredient2"
                },

                new Ingredient
                {
                    Id = 3,
                    Name = "databaseIngredient3"
                },

                new Ingredient
                {
                    Id = 4,
                    Name = "databaseIngredient4"
                },

                new Ingredient
                {
                    Id = 5,
                    Name = "databaseIngredient5"
                },

            };

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Andrei",
                    LastName = "Baciu",
                    Email = "admin@gmail.com",
                    Password = "password",
                    RoleId = Role.Admin,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Plebeul",
                    LastName = "Cartof",
                    Email = "user@gmail.com",
                    Password = "password",
                    RoleId = Role.User,
                }
            };

            _context.Setup(c => c.Menus).ReturnsDbSet(menus);
            _context.Setup(c => c.Recipes).ReturnsDbSet(recipes);
            _context.Setup(c => c.Ingredients).ReturnsDbSet(ingredients);
            _context.Setup(c => c.Meals).ReturnsDbSet(meals);
            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }
    }
}