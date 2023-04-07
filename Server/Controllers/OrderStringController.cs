using Microsoft.AspNetCore.Mvc;

namespace NSCBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderStringController : Controller
    {
        private readonly HttpClient httpClient;
        public OrderStringController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        [HttpGet]
        public async Task<List<Shared.Models.OrderString>> GetAllStringsByOrderListId(int id)
        {
            List<Shared.Models.OrderString> orderStrings = await httpClient.GetFromJsonAsync<List<Shared.Models.OrderString>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllStringsByOrderListId}" + id);
            return orderStrings;
        }


        [HttpPost]
        public async Task<bool> AddOrderString(Shared.Models.OrderString orderString)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddOrderString}", orderString);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        [HttpPut]
        public async Task<bool> EditOrderString(Shared.Models.OrderString orderString)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditOrderString}", orderString);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        [HttpDelete]
        public async Task<bool> DeleteOrderString(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteOrderStringById}" + id);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        [HttpDelete]
        public async Task<bool> DeleteAllStringsForOrder(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteAllStringsForOrder}" + id);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        
    }
}
