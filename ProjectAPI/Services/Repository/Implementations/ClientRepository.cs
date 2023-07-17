using ProjectAPI.DBContext;
using ProjectAPI.Models;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;

namespace ProjectAPI.Services.Repository.Implementations
{
    public class ClientRepository : IBaseRepository<ClientMessage>
    {
        private readonly ApplicationContext db;

        public ClientRepository(ApplicationContext _db)
        {
            db = _db;
        }


        public ClientMessage GetById(int id)
        {
            ClientMessage? clientFromDb = new();

            if (db == null) return clientFromDb;

            try
            {
                clientFromDb = db.Clients.Where(cl => cl.Id == id).Select(client => new ClientMessage()
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
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения обьекта из бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(GetById)} \n" +
                                  $"Error text:{e.Message}");
            }
            return clientFromDb ?? new ClientMessage();
        }

        public List<ClientMessage> GetList()
        {
            List<ClientMessage>? allClientsFromDb = new();
            try
            {
                allClientsFromDb = db.Clients.Select(client => new ClientMessage()
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(GetList)} \n" +
                                  $"Error text:{e.Message}");
            }
            return allClientsFromDb is null ? new() : allClientsFromDb;
        }

        public int Add(ClientMessage entity)
        {
            if (db == null) return int.MinValue;

            try
            {
                ClientDb clientDb = new();

                clientDb.Name = entity.Name;
                clientDb.Surname = entity.Surname;
                clientDb.Country = entity.Country;
                clientDb.City = entity.City;
                clientDb.Cargo = entity.Cargo;
                clientDb.Tel = entity.Tel;
                clientDb.DateRegistration = entity.DateRegistration;

                db.Clients.Add(clientDb);
                db.SaveChanges();
                                               
                int clientIdFromBd = clientDb.Id;
                return clientIdFromBd;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(Add)} \n" +
                                  $"Error text:{e.Message}");
                return int.MinValue;
            }
        }

        public bool Edit(ClientMessage entity)
        {
            if (db == null) return false;

            ClientDb? clientDb = new();

            try
            {
                clientDb = db.Clients.FirstOrDefault(p => p.Id == entity.Id);

                if(clientDb is null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(ClientRepository)} / метод {nameof(Edit)}] : Объект {nameof(ClientDb)} не найден.");
                    return false;
                }

                clientDb.Name = entity.Name;
                clientDb.Surname = entity.Surname;
                clientDb.Country = entity.Country;
                clientDb.City = entity.City;
                clientDb.Cargo = entity.Cargo;
                clientDb.Tel = entity.Tel;

                db.Clients.Update(clientDb);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(Edit)} \n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            if (db == null) return false;

            try
            {
                var client = db.Clients.FirstOrDefault(p => p.Id == id);

                if (client == null)
                {
                    Console.WriteLine($"Ошибка! " +
                                      $"[Класс {nameof(ClientRepository)} / метод {nameof(Delete)}] : Объект {nameof(ClientDb)} не найден.");
                    return false;
                }

                    var orders = db.Orders.Where(x => x.ClientId == client.Id);

                    if(orders == null)
                    {
                        Console.WriteLine($"Ошибка! " +
                                          $"[Класс {nameof(ClientRepository)} / метод {nameof(Delete)}] : " +
                                          $"Связанные заказы для объекта ('{client.Name} {client.Surname}') не найдены.");
                        return false;
                    }

                    foreach (var order in orders)
                    {
                        var stirngs = db.OrderItems.Where(x => x.OrderId == order.Id);
                        db.OrderItems.RemoveRange(stirngs);
                    }
                    db.Orders.RemoveRange(orders);
                

                db.Clients.Remove(client);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(Delete)} \n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }
    }
}