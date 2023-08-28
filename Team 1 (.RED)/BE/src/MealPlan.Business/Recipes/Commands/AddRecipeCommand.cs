using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Recipes.Commands
{
    public class AddRecipeCommand: IRequest<bool>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<int> IngredientIds { get; set; }
    }
}