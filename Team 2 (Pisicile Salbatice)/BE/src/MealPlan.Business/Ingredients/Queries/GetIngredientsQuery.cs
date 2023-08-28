using MealPlan.Business.Ingredients.Models;
using MediatR;

namespace MealPlan.Business.Ingredients.Queries
{
    public class GetIngredientsQuery : IRequest<GetIngredientsModel>
    {
    }
}
