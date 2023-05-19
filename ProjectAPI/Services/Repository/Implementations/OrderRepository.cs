using ProjectAPI.DBContext;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;

namespace ProjectAPI.Services.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext db;

        public OrderRepository(ApplicationContext _db)
        {
            db = _db;
        }

        public OrderMessage GetById(int id)
        {
            OrderMessage? orderList = new();
            try
            {
                orderList = db.Orders.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetById)}\n" +
                                  $"Error text:{e.Message}");
            }
            return orderList is null ? new() : orderList;
        }

        public OrderMessage GetLastCreatedOrderByClientId(int clientId)
        {
            OrderMessage? orderList = new();
            try
            {
                if (db.Orders.Any())
                    orderList = db.Orders.Where(x => x.ClientId == clientId).OrderByDescending(s => s.DateCreate).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения обьекта из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetLastCreatedOrderByClientId)}\n" +
                                  $"Error text:{e.Message}");
            }
            return orderList is null ? new() : orderList;
        }

        public List<OrderMessage> GetAllOrdersByClientId(int clientId)
        {
            List<OrderMessage>? orderLists = new();
            try
            {
                if (db.Orders.Any(x => x.ClientId == clientId))
                {
                    orderLists = db.Orders.Where(x => x.ClientId == clientId).OrderByDescending(t => t).Select(r => r).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetAllOrdersByClientId)}\n" +
                                  $"Error text:{e.Message}");
            }
            return orderLists is null ? new() : orderLists;
        }

        public List<OrderMessage> GetList()
        {
            List<OrderMessage>? allOrders = new();
            try
            {
                allOrders = db.Orders.OrderByDescending(x => x.Id).ToList();
                if (allOrders != null && allOrders.Any())
                {
                    foreach (var order in allOrders)
                    {
                        order.Client = db.Clients.Where(p => p.Id == order.ClientId).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetList)}\n" +
                                  $"Error text:{e.Message}");
            }
            return allOrders is null ? new() : allOrders;
        }



        public bool Add(OrderMessage entity)
        {
            try
            {
                db.Orders.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(Add)}\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Edit(OrderMessage entity)
        {
            try
            {
                var editOrderList = db.Orders.FirstOrDefault(p => p.Id == entity.Id);

                if (editOrderList == null)
                    return false;

                db.Orders.Update(editOrderList);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка редактирования обьекта в бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(Edit)}\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderList = db.Orders.FirstOrDefault(p => p.Id == id);
                if (orderList == null)
                    return false;

                db.Orders.Remove(orderList);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(Delete)}\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }
    }
}