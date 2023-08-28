using FluentValidation;
using MealPlan.API.Requests.Meals;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan.API.Requests.Menus
{
    public class AddMenuRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuCategory CategoryId { get; set; }

        public List<AddMealRequest> Meals { get; set; }
    }

    public class AddMenuRequestValidator : AbstractValidator<AddMenuRequest>
    {
        public AddMenuRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(256);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .Must(CategoryId => (int)CategoryId > 0).WithMessage("Category id should be greater than 0")
                .IsInEnum();

            RuleFor(x => x.Meals)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(mealList => mealList.TrueForAll(m => m != null))
                .WithMessage("Meals cannot be null")
                .Must(mealList => mealList.Count == Enum.GetNames(typeof(MealType)).Length)
                .WithMessage("The number of meals must match the number of meal types.")
                .Must(mealList => mealList.DistinctBy(m => m.MealTypeId).Count() == mealList.Count)
                .WithMessage("Meals should be unique, one of each of the 5 types")
                .Must(mealList => mealList.DistinctBy(m => m.RecipeId).Count() == mealList.Count)
                .WithMessage("Recipe IDs must be unique.")
                .ForEach(x => x.SetValidator(new AddMealRequestValidator()));
        }
    }
}