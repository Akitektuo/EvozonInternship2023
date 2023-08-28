using FluentValidation;
using MealPlan.API.Requests.Utils;
using MealPlan.Data.Models.Menus;

namespace MealPlan.API.Requests.Menus
{
    public class GetAllMenusRequest : PageRequest
    {
        public MenuCategory CategoryId { get; set; }
    }

    public class GetAllMenusRequestValidator : AbstractValidator<GetAllMenusRequest>
    {
        public GetAllMenusRequestValidator() 
        {
            RuleFor(x => x.CategoryId).NotEmpty().IsInEnum();

            Include(new PageRequestValidator());
        }
    }
}