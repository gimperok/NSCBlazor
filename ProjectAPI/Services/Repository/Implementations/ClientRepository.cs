using ProjectAPI.DBContext;
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
            ClientMessage? client = new();
            try
            {
                client = db.Clients.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения обьекта из бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(GetById)} \n" +
                                  $"Error text:{e.Message}");
            }
            return client is null ? new() : client;
        }

        public List<ClientMessage> GetList()
        {
            List<ClientMessage>? allClients = new();
            try
            {
                allClients = db.Clients.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(GetList)} \n" +
                                  $"Error text:{e.Message}");
            }
            return allClients is null ? new() : allClients;
        }

        public bool Add(ClientMessage entity)
        {
            try
            {
                db.Clients.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд.\n" +
                                  $"Место: {nameof(ClientRepository)}/{nameof(Add)} \n" +
                                  $"Error text:{e.Message}");
                return false;
            }
        }

        public bool Edit(ClientMessage entity)
        {
            ClientMessage editClient = new();
            try
            {
                editClient = db.Clients.FirstOrDefault(p => p.Id == entity.Id);
                if (editClient != null)
                {
                    editClient.Name = entity.Name;
                    editClient.Surname = entity.Surname;
                    editClient.Country = entity.Country;
                    editClient.City = entity.City;
                    editClient.Cargo = entity.Cargo;
                    editClient.Tel = entity.Tel;

                    db.Clients.Update(editClient);
                    db.SaveChanges();
                    return true;
                }
                return false;
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
            try
            {
                var client = db.Clients.FirstOrDefault(p => p.Id == id);
                if (client == null)
                    return false;

                var orders = db.Orders.Where(x => x.ClientId == client.Id);
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        var stirngs = db.OrderItems.Where(x => x.OrderId == order.Id);
                        db.OrderItems.RemoveRange(stirngs);
                    }
                    db.Orders.RemoveRange(orders);
                }

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