using FluentValidation;
using MealPlan.API.Requests.Shared;
using MealPlan.Data.Models.Meals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.API.Requests.Menus
{
    public class AddMenuRequest
    {
        public string MenuName { get; set; }
        public MenuType MenuTypeId { get; set; }
        public List<MealReceived> Meals { get; set; } = new List<MealReceived>();
    }

    public class AddMenuRequestValidator : AbstractValidator<AddMenuRequest> { 
        
        public AddMenuRequestValidator() { 
            RuleFor(x => x.MenuName)
                .NotEmpty()
                .Matches(RegexConstants.Name)
                .MaximumLength(50);
            RuleFor(x => x.MenuTypeId)
                .NotEmpty()
                .IsInEnum();
            RuleFor(x => x.Meals)
                .NotEmpty();
            RuleFor(x => x.Meals)
                .Must(meals => meals.Count == 5)
                .WithMessage($"Each menu requires {Enum.GetValues(typeof(MealType)).Length} meals.")
                .Must(meals => !meals.GroupBy(meal => meal.MealTypeId).Any(group => group.Count() > 1))
                .WithMessage("A menu requires a meal from each meal category.")
                .Must(meals => meals.Select(m => m.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList().Count == meals.Count)
                .WithMessage("There are duplicate meal names sent.")
                .Must(meals => meals.DistinctBy(meal => meal.RecipeId).ToList().Count == meals.Count)
                .WithMessage("There are duplicate recipes sent.");
            RuleForEach(x => x.Meals)
                .SetValidator(new MealReceivedValidator());
        }
    }
}
