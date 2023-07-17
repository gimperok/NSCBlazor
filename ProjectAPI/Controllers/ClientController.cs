using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IBaseRepository<ClientMessage> clientRepository;

        public ClientController(IBaseRepository<ClientMessage> _repository)
        {
            clientRepository = _repository;
        }


        /// <summary>
        /// Получить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Обьект клиента</returns>
        [HttpGet]
        public ClientMessage GetClientById(int id)
        {
            return clientRepository.GetById(id);   
        }

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns>Список всех клиентов</returns>
        [HttpGet]
        public List<ClientMessage> GetAllClients()
        {
            return clientRepository.GetList();
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="client">Обьект клиента</param>
        [HttpPost]
        public int AddClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return int.MinValue;
            return clientRepository.Add(client);
        }

        /// <summary>
        /// Изменить обьект клиента
        /// </summary>
        /// <param name="client">Обьект клиента</param>
        [HttpPut]
        public bool EditClient(ClientMessage client)
        {
            if (!ModelState.IsValid)
                return false;
            return clientRepository.Edit(client);
        }

        /// <summary>
        /// Удалить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        [HttpDelete]
        public bool DeleteClientById(int id)
        {
            return clientRepository.Delete(id);
        }
    }
}