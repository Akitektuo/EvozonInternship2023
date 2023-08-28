using MealPlan.API.Requests.Shared;
using FluentValidation;
using MealPlan.Data.Models.Meals;

namespace MealPlan.API.Requests.Menus
{
    public class GetAllMenusRequest
    {
        public PaginationModel PaginationModel { get; set; } = new PaginationModel();
        public MenuType Category { get; set; }
    }

    public class GetAllMenusRequestValidator : AbstractValidator<GetAllMenusRequest>
    {
        public GetAllMenusRequestValidator()
        {
            RuleFor(x => x.PaginationModel).SetValidator(new PaginationModelValidator());
            RuleFor(x => x.Category).IsInEnum();
        }
    }
}
