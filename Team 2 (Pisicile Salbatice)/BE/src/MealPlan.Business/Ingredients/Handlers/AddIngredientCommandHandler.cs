using MealPlan.Business.Exceptions;
using MealPlan.Business.Ingredients.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Ingredients.Handlers
{
    public class AddIngredientCommandHandler : IRequestHandler<AddIngredientCommand, bool>
    {
        private readonly MealPlanContext _context;

        public AddIngredientCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var isDuplicateName = await _context.Ingredients.Where(x => x.Name == request.IngredientName).AnyAsync();

            if (isDuplicateName)
            {
                throw new CustomApplicationException(ErrorCode.IngredientAlreadyExists, $"The ingredient '{request.IngredientName}' already exists.");
            }

            var newIngredient = new Ingredient { Name = request.IngredientName };

            await _context.AddAsync(newIngredient);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
