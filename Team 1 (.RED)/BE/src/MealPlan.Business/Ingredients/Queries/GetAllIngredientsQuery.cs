using MealPlan.Business.Ingredients.Models;
using MediatR;
using System.Collections.Generic;

namespace MealPlan.Business.Ingredients.Queries
{
    public class GetAllIngredientsQuery : IRequest<List<IngredientModel>>
    {
    }
}