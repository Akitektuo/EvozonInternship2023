using MealPlan.Business.Ingredients.Models;
using MealPlan.Business.Ingredients.Queries;
using MealPlan.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Ingredients.Handlers
{
    public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, GetIngredientsModel>
    {
        private readonly MealPlanContext _context;

        public GetIngredientsQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<GetIngredientsModel> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            return new GetIngredientsModel
            {
                IngredientsList = await GetIngredients()
            };
        }

        private async Task<List<IngredientModel>> GetIngredients()
        {
            return await _context.Ingredients
                .ToIngredientModel()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
