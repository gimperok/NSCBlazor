using Microsoft.AspNetCore.Mvc;
using NSCBlazor.Shared.Models;
using System.Text.Json;

namespace NSCBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderListController : Controller
    {
        private readonly HttpClient httpClient;
        public OrderListController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        [HttpGet] //ЗАКАЗ ПО ID
        public async Task<OrderList> GetOrderById(int id)
        {
            OrderList? orderList = await httpClient.GetFromJsonAsync<OrderList>($"{AppSettings.GetApiUrl}{AppSettings.GetOrderById}{id}");
            return orderList;
        }



        [HttpGet] //ПОСЛЕДНИЙ СОЗДАННЫЙ
        public async Task<OrderList> GetLastCreatedOrderListByUserId(int id)
        {
            OrderList? orderList = await httpClient.GetFromJsonAsync<OrderList>($"{AppSettings.GetApiUrl}{AppSettings.GetLastCreatedOrderListByUserId}{id}");
            return orderList;
        }


        [HttpGet] //СПИСОК ПО USER.ID
        public async Task<List<OrderList>> GetAllOrderListsByUserId(int clientId)
        {
            List<OrderList> orderList = new List<OrderList>();
            if (clientId > 0)
                orderList  = await httpClient.GetFromJsonAsync<List<OrderList>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllOrderListsByUserId}{clientId}");
            return orderList;
        }


        [HttpPost]
        public async Task<bool> AddOrderList(OrderList orderList)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddOrderList}", orderList);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }




        [HttpPut]
        public async Task<bool> EditOrderList(OrderList orderList)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditOrderList}", orderList);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }



        [HttpDelete]
        public async Task<bool> DeleteOrderList(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteOrderListById}" + id);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
