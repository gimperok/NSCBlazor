using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectJson.Models;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationContext db;

        public ClientController(ApplicationContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Получить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Обьект клиента</returns>
        [HttpGet]
        public ClientMessage GetClientById(int id)
        {
            ClientMessage? client = null;
            try
            {
                client = db.Clients.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения обьекта из бд. Место: ClientController. Error text:{e.Message}");
            }
            return client;
        }

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns>Список всех клиентов</returns>
        [HttpGet]
        public List<ClientMessage> GetAllClients()
        {
            List<ClientMessage>? allClients = null;
            try
            {
                allClients = db.Clients.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд. Место: ClientController. Error text:{e.Message}");
            }
            return allClients;
        }


        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="client">Обьект клиента</param>
        [HttpPost]
        public bool AddClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return false;

            try
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд. Место: ClientController. Error text:{e.Message}");
                return false;
            }            
        }

        /// <summary>
        /// Изменить обьект клиента
        /// </summary>
        /// <param name="editClient">Обьект клиента</param>
        [HttpPut]
        public bool EditClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return false;

            ClientMessage? editClient = null;
            try
            {
                editClient = db.Clients.FirstOrDefault(p => p.Id == client.Id);
                if(editClient != null)
                {
                    editClient.Name = client.Name;
                    editClient.Surname = client.Surname;
                    editClient.Country = client.Country;
                    editClient.City = client.City;
                    editClient.Cargo = client.Cargo;
                    editClient.Tel = client.Tel;

                    db.Clients.Update(editClient);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка записи обьекта в бд. Место: ClientController. Error text:{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Удалить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        [HttpDelete]
        public bool DeleteClientById(int id)
        {
            try
            {
                var client = db.Clients.FirstOrDefault(p => p.Id == id);
                if (client == null)
                    return false;

                var orders = db.Orders.Where(x => x.ClientId == client.Id);
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
                Console.WriteLine($"Ошибка удаления обьекта из бд. Место: ClientController. Error text:{e.Message}");
                return false;
            }
        }
    }
}