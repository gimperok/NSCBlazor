using Microsoft.AspNetCore.Mvc;
using NSCBlazor.Client.Helpers;
using ProjectJson.Models;
using SharedClient = NSCBlazor.Shared.Models.Client;


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
        public async Task<SharedClient> GetClientById(int id)
        {
            SharedClient? client = await httpClient.GetFromJsonAsync<SharedClient>($"{AppSettings.GetApiUrl}{AppSettings.GetClientById}" + id);
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
        public async Task<int> AddClient(ClientMessage client)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddClient}", client);

            var saveRes = await WebServiceHelper.GetContentFromResponse<int>(response);
            return saveRes;
        }

        /// <summary>
        /// Изменить обьект клиента
        /// </summary>
        /// <param name="editClient">Обьект клиента</param>
        [HttpPut]
        public async Task<bool> EditClient(ClientMessage editClient)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditClient}", editClient);

            var updateRes = await WebServiceHelper.GetContentFromResponse<bool>(response);

            return updateRes;
        }

        /// <summary>
        /// Удалить клиента по его ID
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        [HttpDelete]
        public async Task<bool> DeleteClient(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteClientById}" + id);

            var deleteRes = await WebServiceHelper.GetContentFromResponse<bool>(response);

            return deleteRes;
        }
    }
}