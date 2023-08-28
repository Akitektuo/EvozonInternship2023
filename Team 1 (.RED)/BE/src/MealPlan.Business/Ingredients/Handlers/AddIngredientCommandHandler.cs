using MealPlan.Business.Exceptions;
using MealPlan.Business.Ingredients.Commands;
using MealPlan.Data;
using MealPlan.Data.Models.Recipes;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MealPlan.Business.Ingredients.Handlers
{
    public class AddIngredientCommandHandler : IRequestHandler<AddIngredientCommand, bool>
    {
        private MealPlanContext _context;

        public AddIngredientCommandHandler(MealPlanContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            if (IngredientAlreadyExist(request))
            {
                throw new CustomApplicationException(ErrorCode.IngredientAlreadyExist, "Ingredient already exist");
            }

            var ingredient = new Ingredient
            {
                Name = request.Name
            };

            await _context.Ingredients.AddAsync(ingredient);

            return await _context.SaveChangesAsync() > 0;
        }

        private bool IngredientAlreadyExist(AddIngredientCommand request)
        {
            return _context.Ingredients.Where(x => x.Name == request.Name).Any();
        }
    }
}