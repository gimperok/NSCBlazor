using ProjectAPI.DBContext;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;

namespace ProjectAPI.Services.Repository.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationContext db;

        public OrderItemRepository(ApplicationContext _db)
        {
            db = _db;
        }


        public List<OrderItemMessage> GetListItemsByOrderId(int orderId)
        {
            List<OrderItemMessage>? orderStringsList = new();
            try
            {
                if (db.OrderItems.Any(p => p.OrderId == orderId))
                    orderStringsList = db.OrderItems.Where(p => p.OrderId == orderId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(GetListItemsByOrderId)}.\n" +
                                  $"Error text:{e.Message}");
            }
            return orderStringsList is null ? new() : orderStringsList;
        }

        public bool Add(OrderItemMessage entity)
        {
            try
            {
                db.OrderItems.Add(entity);

                var order = db.Orders.Where(x => x.Id == entity.OrderId).FirstOrDefault();
                if (order != null)
                {
                    order.DateModify = DateTime.Now;

                    db.Orders.Update(order);

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка добавления обьекта в бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(Add)}.\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Edit(OrderItemMessage entity)
        {
            try
            {
                var currentOrderString = db.OrderItems.FirstOrDefault(p => p.Id == entity.Id);

                if (currentOrderString == null)
                    return false;

                currentOrderString.Kod = entity.Kod;
                currentOrderString.Leather = entity.Leather;
                currentOrderString.Color = entity.Color;

                currentOrderString.Size35 = entity.Size35;
                currentOrderString.Size36 = entity.Size36;
                currentOrderString.Size37 = entity.Size37;
                currentOrderString.Size38 = entity.Size38;
                currentOrderString.Size39 = entity.Size39;
                currentOrderString.Size40 = entity.Size40;
                currentOrderString.Size41 = entity.Size41;

                currentOrderString.Price = entity.Price;
                currentOrderString.Note = entity.Note;


                db.OrderItems.Update(currentOrderString);

                var order = db.Orders.Where(x => x.Id == currentOrderString.OrderId).FirstOrDefault();
                if (order != null)
                {
                    order.DateModify = DateTime.Now;

                    db.Orders.Update(order);

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка изменения обьекта в бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(Edit)}.\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderString = db.OrderItems.FirstOrDefault(p => p.Id == id);
                if (orderString == null)
                    return false;

                db.OrderItems.Remove(orderString);

                var order = db.Orders.Where(x => x.Id == orderString.OrderId).FirstOrDefault();
                if (order != null)
                {
                    order.DateModify = DateTime.Now;

                    db.Orders.Update(order);

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(Delete)}.\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool DeleteListItemsByOrderId(int id)
        {
            try
            {
                var stringList = db.OrderItems.Where(p => p.OrderId == id).ToList();

                if (stringList?.Count > 0)
                {
                    foreach (var str in stringList)
                    {
                        db.OrderItems.Remove(str);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(DeleteListItemsByOrderId)}.\n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }


        [Obsolete("Метод не используется")]
        public OrderItemMessage GetById(int id)
        {
            OrderItemMessage? orderItem = new();
            try
            {
                orderItem = db.OrderItems.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения обьекта из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(GetById)}.\n" +
                                  $"Error text:{e.Message}");
            }
            return orderItem is null ? new() : orderItem;
        }

        [Obsolete("Метод не используется")]
        public List<OrderItemMessage> GetList()
        {
            List<OrderItemMessage>? allOrdersStrings = new();
            try
            {
                allOrdersStrings = db.OrderItems.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(GetList)}.\n" +
                                  $"Error text:{e.Message}");
            }
            return allOrdersStrings is null ? new() : allOrdersStrings;
        }
    }
}