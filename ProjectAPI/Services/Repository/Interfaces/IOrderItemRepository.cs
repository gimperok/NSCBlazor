using ProjectJson.Models;

namespace ProjectAPI.Services.Repository.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItemMessage>
    {
        List<OrderItemMessage> GetListItemsByOrderId(int orderId);
        bool DeleteListItemsByOrderId(int orderId);
    }
}