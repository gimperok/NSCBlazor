using ProjectAPI.DBContext;
using ProjectAPI.Models;
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
            OrderMessage? orderFromDb = new();
            try
            {
                orderFromDb = db.Orders.Where(x => x.Id == id).Select(ord => new OrderMessage()
                {
                    Id = ord.Id,
                    DateCreate = ord.DateCreate,
                    DateModify = ord.DateModify,
                    ClientId = ord.ClientId,
                }).FirstOrDefault();

                if(orderFromDb != null)
                {
                    orderFromDb.Client = db.Clients.Where(cl => cl.Id == orderFromDb.ClientId)
                    .Select(client => new ClientMessage()
                    {
                        Id = client.Id,
                        Name = client.Name,
                        Surname = client.Surname,
                        Country = client.Country,
                        City = client.City,
                        Cargo = client.Cargo,
                        Tel = client.Tel,
                        DateRegistration = client.DateRegistration
                    }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetById)}\n" +
                                  $"Error text:{e.Message}");
            }
            return orderFromDb is null ? new() : orderFromDb;
        }

        public OrderMessage GetLastCreatedOrderByClientId(int clientId)
        {
            OrderMessage? orderList = new();
            try
            {
                if (db.Orders.Any())
                    orderList = db.Orders.Where(x => x.ClientId == clientId).Select(ord => new OrderMessage()
                    {
                        Id = ord.Id,
                        DateCreate = ord.DateCreate,
                        DateModify = ord.DateModify,
                        ClientId = ord.ClientId,
                    }).OrderByDescending(s => s.DateCreate).FirstOrDefault();
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
                    ClientMessage? currentClient = db.Clients.Where(cl => cl.Id == clientId)
                        .Select(client => new ClientMessage()
                        {
                            Id = client.Id,
                            Name = client.Name,
                            Surname = client.Surname,
                            Country = client.Country,
                            City = client.City,
                            Cargo = client.Cargo,
                            Tel = client.Tel,
                            DateRegistration = client.DateRegistration
                        }).FirstOrDefault();

                    orderLists = db.Orders.Where(x => x.ClientId == clientId).Select(ord => new OrderMessage()
                    {
                        Id = ord.Id,
                        DateCreate = ord.DateCreate,
                        DateModify = ord.DateModify,
                        ClientId = ord.ClientId,
                        Client = currentClient
                    }).OrderByDescending(t =>t.DateCreate).ToList();
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
                List<ClientMessage> clientsList = db.Clients
                    .Select(client => new ClientMessage()
                    {
                        Id = client.Id,
                        Name = client.Name,
                        Surname = client.Surname,
                        Country = client.Country,
                        City = client.City,
                        Cargo = client.Cargo,
                        Tel = client.Tel,
                        DateRegistration = client.DateRegistration
                    }).ToList();

                allOrders = db.Orders.Select(ord => new OrderMessage()
                {
                    Id = ord.Id,
                    DateCreate = ord.DateCreate,
                    DateModify = ord.DateModify,
                    ClientId = ord.ClientId,
                }).OrderByDescending(x => x.Id).ToList();

                allOrders.ForEach(ord => ord.Client = clientsList.FirstOrDefault(cl => cl.Id == ord.ClientId));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(GetList)}\n" +
                                  $"Error text:{e.Message}");
            }
            return allOrders is null ? new() : allOrders;
        }



        public int Add(OrderMessage entity)
        {
            if (db == null) return int.MinValue;

            try
            {
                OrderDb orderDb = new();

                orderDb.DateCreate = entity.DateCreate;
                orderDb.ClientId = entity.ClientId;

                db.Orders.Add(orderDb);
                db.SaveChanges();

                return orderDb.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд.\n" +
                                  $"Место: {nameof(OrderRepository)}/{nameof(Add)}\n" +
                                  $"Error text:{e.Message}");
                return int.MinValue;
            }
        }

        public bool Edit(OrderMessage entity)
        {
            if (db == null) return false;

            OrderDb orderDb = new();

            try
            {
                orderDb = db.Orders.FirstOrDefault(p => p.Id == entity.Id);

                if (orderDb == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderRepository)} / метод {nameof(Edit)}] : Объект {nameof(OrderMessage)} не найден.");
                    return false;
                }

                orderDb.DateCreate = entity.DateCreate;
                orderDb.DateModify = entity.DateModify;
                orderDb.ClientId = entity.ClientId;

                db.Orders.Update(orderDb);
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
            if (db == null) return false;

            try
            {
                var orderList = db.Orders.FirstOrDefault(p => p.Id == id);
                if (orderList == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(OrderRepository)} / метод {nameof(Delete)}] : Объект {nameof(OrderMessage)} не найден.");
                    return false;
                }

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