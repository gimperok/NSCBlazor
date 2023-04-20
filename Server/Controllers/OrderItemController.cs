using Microsoft.AspNetCore.Mvc;
using ProjectJson.Models;

namespace NSCBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderItemController : Controller
    {
        private readonly HttpClient httpClient;
        public OrderItemController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        [HttpGet]
        public async Task<List<OrderItemMessage>> GetAllStringsByOrderListId(int id)
        {
            List<OrderItemMessage> orderStrings = await httpClient.GetFromJsonAsync<List<OrderItemMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllStringsByOrderListId}" + id);
            return orderStrings;
        }


        [HttpPost]
        public async Task<bool> AddOrderString(OrderItemMessage orderString)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddOrderString}", orderString);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        [HttpPut]
        public async Task<bool> EditOrderString(OrderItemMessage orderString)
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
