using ProjectJson.Models;

namespace ProjectAPI.Services.Repository.Interfaces
{
    public interface IOrderRepository : IBaseRepository<OrderMessage>
    {
        OrderMessage GetLastCreatedOrderByClientId(int clientId);
        List<OrderMessage> GetAllOrdersByClientId(int clientId);
    }
}