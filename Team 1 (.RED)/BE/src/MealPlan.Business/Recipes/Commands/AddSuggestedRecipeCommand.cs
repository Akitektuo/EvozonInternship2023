using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Recipes.Commands
{
    public class AddSuggestedRecipeCommand : IRequest<bool>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
    }
}