using ProjectAPI.DBContext;
using ProjectAPI.Models;
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
                {
                    var orderFromDb = db.Orders.Where(x => x.Id == orderId).Select(ord => new OrderMessage()
                    {
                        Id = ord.Id,
                        DateCreate = ord.DateCreate,
                        DateModify = ord.DateModify,
                        ClientId = ord.ClientId,
                    }).FirstOrDefault();

                    orderStringsList = db.OrderItems.Where(p => p.OrderId == orderId).Select(item => new OrderItemMessage()
                    {
                        Id = item.Id,
                        Kod = item.Kod,
                        Leather = item.Leather,
                        Color = item.Color,
                        Size35 = item.Size35,
                        Size36 = item.Size36,
                        Size37 = item.Size37,
                        Size38 = item.Size38,
                        Size39 = item.Size39,
                        Size40 = item.Size40,
                        Size41 = item.Size41,
                        Price = item.Price,
                        Note = item.Note,
                        OrderId = orderId,
                        Order = orderFromDb
                    }).ToList();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(GetListItemsByOrderId)}.\n" +
                                  $"Error text:{e.Message}");
            }
            return orderStringsList is null ? new() : orderStringsList;
        }

        public int Add(OrderItemMessage entity)
        {
            if (db == null) return int.MinValue;

            OrderItemDb orderItemDb = new();

            try
            {
                orderItemDb.Kod = entity.Kod;
                orderItemDb.Leather = entity.Leather;
                orderItemDb.Color = entity.Color;
                orderItemDb.Size35 = entity.Size35;
                orderItemDb.Size36 = entity.Size36;
                orderItemDb.Size37 = entity.Size37;
                orderItemDb.Size38 = entity.Size38;
                orderItemDb.Size39 = entity.Size39;
                orderItemDb.Size40 = entity.Size40;
                orderItemDb.Size41 = entity.Size41;
                orderItemDb.Price = entity.Price;
                orderItemDb.Note = entity.Note;
                orderItemDb.OrderId = entity.OrderId;

                db.OrderItems.Add(orderItemDb);

                var order = db.Orders.Where(x => x.Id == orderItemDb.OrderId).FirstOrDefault();

                if(order == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderItemRepository)} / метод {nameof(Add)}] : Объект {nameof(OrderMessage)} не найден.");
                    return int.MinValue;
                }

                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return orderItemDb.Id;
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка добавления обьекта в бд.\n" +
                                  $"Место: {nameof(OrderItemRepository)}/{nameof(Add)}.\n" +
                                  $"Error text:{e.Message}");
                return int.MinValue;
            }
        }

        public bool Edit(OrderItemMessage entity)
        {
            if (db == null) return false;

            OrderItemDb orderItemDb = new();

            try
            {

                orderItemDb = db.OrderItems.FirstOrDefault(p => p.Id == entity.Id);

                if (orderItemDb == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderItemRepository)} / метод {nameof(Edit)}] : Объект {nameof(OrderItemMessage)} не найден.");
                    return false;
                }

                orderItemDb.Kod = entity.Kod;
                orderItemDb.Leather = entity.Leather;
                orderItemDb.Color = entity.Color;

                orderItemDb.Size35 = entity.Size35;
                orderItemDb.Size36 = entity.Size36;
                orderItemDb.Size37 = entity.Size37;
                orderItemDb.Size38 = entity.Size38;
                orderItemDb.Size39 = entity.Size39;
                orderItemDb.Size40 = entity.Size40;
                orderItemDb.Size41 = entity.Size41;

                orderItemDb.Price = entity.Price;
                orderItemDb.Note = entity.Note;
                orderItemDb.OrderId = entity.OrderId;


                db.OrderItems.Update(orderItemDb);

                var order = db.Orders.Where(x => x.Id == orderItemDb.OrderId).FirstOrDefault();

                if(order == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderItemRepository)} / метод {nameof(Edit)}] : Объект {nameof(OrderMessage)} не найден.");
                    return false;
                }

                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return true;
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
            if (db == null) return false;

            try
            {
                var orderString = db.OrderItems.FirstOrDefault(p => p.Id == id);
                if (orderString == null)
                    return false;

                db.OrderItems.Remove(orderString);

                var order = db.Orders.Where(x => x.Id == orderString.OrderId).FirstOrDefault();
                if(order == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderItemRepository)} / метод {nameof(Delete)}] : Объект {nameof(OrderMessage)} не найден.");
                    return false;
                }

                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return true;                
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
            if (db == null) return false;

            try
            {
                var stringList = db.OrderItems.Where(p => p.OrderId == id).ToList();

                if (stringList == null)
                    return false;

                if (stringList.Count > 0)
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
                orderItem = db.OrderItems.Where(p => p.Id == id).Select(item => new OrderItemMessage()
                {
                    Kod = item.Kod,
                    Leather = item.Leather,
                    Color = item.Color,
                    Size35 = item.Size35,
                    Size36 = item.Size36,
                    Size37 = item.Size37,
                    Size38 = item.Size38,
                    Size39 = item.Size39,
                    Size40 = item.Size40,
                    Size41 = item.Size41,
                    Price = item.Price,
                    Note = item.Note,
                    OrderId = item.OrderId,
                }).FirstOrDefault();
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
                allOrdersStrings = db.OrderItems.Select(item => new OrderItemMessage()
                {
                    Kod = item.Kod,
                    Leather = item.Leather,
                    Color = item.Color,
                    Size35 = item.Size35,
                    Size36 = item.Size36,
                    Size37 = item.Size37,
                    Size38 = item.Size38,
                    Size39 = item.Size39,
                    Size40 = item.Size40,
                    Size41 = item.Size41,
                    Price = item.Price,
                    Note = item.Note,
                    OrderId = item.OrderId,
                }).ToList();
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