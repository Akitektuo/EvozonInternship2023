using System.Threading.Tasks;

namespace MealPlan.Business.Services
{
    public interface IOrderStatusService
    {
        Task<bool> ApproveOrder(int orderId);
        Task<bool> RejectOrder(int orderId);
    }
}