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
    public class GetAllIngredientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, List<IngredientModel>>
    {
        private MealPlanContext _context;

        public GetAllIngredientsQueryHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<List<IngredientModel>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Ingredients
                .Select(x => new IngredientModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return result;
        }
    }
}