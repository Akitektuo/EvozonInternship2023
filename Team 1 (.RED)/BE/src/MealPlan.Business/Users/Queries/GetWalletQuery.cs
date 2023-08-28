using MediatR;

namespace MealPlan.Business.Users.Queries
{
    public class GetWalletQuery : IRequest<double>
    {
        public string Email { get; set; }
    }
}