using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


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


        [HttpGet]
        public async Task<Shared.Models.Client> GetClientById(int id)
        {
            Shared.Models.Client? client = await httpClient.GetFromJsonAsync<Shared.Models.Client>($"{AppSettings.GetApiUrl}{AppSettings.GetClientById}" + id);
            return client;
        }


        [HttpGet]
        public async Task<List<Shared.Models.Client>> GetAllClients()
        {
            List<Shared.Models.Client>? allClients = await httpClient.GetFromJsonAsync<List<Shared.Models.Client>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllClients}");
            return allClients;
        }


        [HttpPost]
        public async Task<bool> AddClient(Shared.Models.Client client)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddClient}", client);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        [HttpPut]
        public async Task<bool> EditClient(Shared.Models.Client editClient)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditClient}", editClient);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


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
