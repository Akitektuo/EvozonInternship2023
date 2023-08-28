using FluentValidation;
using MealPlan.Data.Models.Meals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.API.Requests.Menus.AddGeneratedMenu
{
    public class AddGeneratedMenuRequest
    {
        public string Name { get; set; }
        public MenuType MenuTypeId { get; set; }
        public List<GeneratedMeal> Meals { get; set; } = new List<GeneratedMeal>();
    }

    public class AddGeneratedMenuRequestValidator : AbstractValidator<AddGeneratedMenuRequest>
    {
        public AddGeneratedMenuRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.MenuTypeId)
                .NotEmpty()
                .IsInEnum();
            RuleFor(x => x.Meals)
                .NotEmpty();
            RuleFor(x => x.Meals)
                .Must(meals => meals.Count == Enum.GetValues(typeof(MealType)).Length)
                .WithMessage($"Each menu requires {Enum.GetValues(typeof(MealType)).Length} meals.")
                .Must(meals => !meals.GroupBy(meal => meal.MealTypeId).Any(group => group.Count() > 1))
                .WithMessage("A menu requires a meal from each meal category.")
                .Must(meals => meals.Select(m => m.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList().Count == meals.Count)
                .WithMessage("There are duplicate meal names sent.")
                .Must(meals => meals.DistinctBy(meal => meal.Recipe).ToList().Count == meals.Count)
                .WithMessage("There are duplicate recipes sent.");
            RuleForEach(x => x.Meals)
                .SetValidator(new GeneratedMealValidator());
        }
    }
}
