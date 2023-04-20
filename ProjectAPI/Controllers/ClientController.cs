using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectJson.Models;
using System.Text.Json;

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


        [HttpGet]
        public List<ClientMessage> GetAllClients() => db.Clients.ToList();

        [HttpGet]
        public ClientMessage GetClientById(int id)
        {            
            var client = db.Clients.FirstOrDefault(p => p.Id == id);
            return client;
        }


        [HttpPost]
        public bool AddClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return false;

            db.Clients.Add(client);
            db.SaveChanges();
            return true;
        }


        [HttpPut]
        public bool EditClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return false;

            var editClient = db.Clients.FirstOrDefault(p => p.Id == client.Id);

            if(editClient == null) 
                return false;

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


        [HttpDelete]
        public bool DeleteClientById(int id)
        {
            var client = db.Clients.FirstOrDefault(p => p.Id == id);
            if (client == null)
                return false;

            db.Clients.Remove(client);
            db.SaveChanges();
            return true;
        }
    }
}