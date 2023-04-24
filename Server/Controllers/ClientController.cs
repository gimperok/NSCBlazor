using Microsoft.AspNetCore.Mvc;
using ProjectJson.Models;


namespace NSCBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly HttpClient httpClient;
        public ClientController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        /// <summary>
        /// Получить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Обьект клиента</returns>
        [HttpGet]
        public async Task<ClientMessage> GetClientById(int id)
        {
            ClientMessage? client = await httpClient.GetFromJsonAsync<ClientMessage>($"{AppSettings.GetApiUrl}{AppSettings.GetClientById}" + id);
            return client;
        }

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        /// <returns>Список всех клиентов</returns>
        [HttpGet]
        public async Task<List<ClientMessage>> GetAllClients()
        {
            List<ClientMessage>? allClients = await httpClient.GetFromJsonAsync<List<ClientMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllClients}");
            return allClients;
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="client">Обьект клиента</param>
        [HttpPost]
        public async Task<bool> AddClient(ClientMessage client)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddClient}", client);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        /// <summary>
        /// Изменить обьект клиента
        /// </summary>
        /// <param name="editClient">Обьект клиента</param>
        [HttpPut]
        public async Task<bool> EditClient(ClientMessage editClient)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditClient}", editClient);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        /// <summary>
        /// Удалить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        [HttpDelete]
        public async Task<bool> DeleteClient(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteClientById}" + id);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}