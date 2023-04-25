using Microsoft.AspNetCore.Mvc;
using NSCBlazor.Shared.Models;
using ProjectJson.Models;
using System.Text.Json;


namespace NSCBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly HttpClient httpClient;
        public OrderController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        /// <summary>
        /// Получить заказ по его ID
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Обьект заказа</returns>
        [HttpGet]
        public async Task<OrderMessage> GetOrderById(int id)
        {
            OrderMessage? orderList = await httpClient.GetFromJsonAsync<OrderMessage>($"{AppSettings.GetApiUrl}{AppSettings.GetOrderById}{id}");
            return orderList;
        }


        /// <summary>
        /// Получить последний в списке заказ по ID клиента
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Последний в списке заказ</returns>
        [HttpGet]
        public async Task<OrderMessage> GetLastCreatedOrderListByUserId(int id)
        {
            OrderMessage? orderList = await httpClient.GetFromJsonAsync<OrderMessage>($"{AppSettings.GetApiUrl}{AppSettings.GetLastCreatedOrderListByUserId}{id}");
            return orderList;
        }


        /// <summary>
        /// Список заказов по ID клиента
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Список заказов</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrderListsByUserId(int clientId)
        {
            List<OrderMessage> orderList = new List<OrderMessage>();
            if (clientId > 0)
                orderList  = await httpClient.GetFromJsonAsync<List<OrderMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllOrderListsByUserId}{clientId}");
            return orderList;
        }


        /// <summary>
        /// Получить весь список заказов из БД
        /// </summary>
        /// <returns>Весь список заказов из БД</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrdersFromDb()
        {         
            var orderList = await httpClient.GetFromJsonAsync<List<OrderMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllOrdersFromDb}");

            return orderList;
        }


        /// <summary>
        /// Добавить заказ
        /// </summary>
        /// <param name="orderList">Обьект заказа</param>
        [HttpPost]
        public async Task<bool> AddOrderList(OrderMessage orderList)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddOrderList}", orderList);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        /// <summary>
        /// Изменить обьект заказа
        /// </summary>
        /// <param name="orderList">Обьект заказа</param>
        [HttpPut]
        public async Task<bool> EditOrderList(OrderMessage orderList)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditOrderList}", orderList);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }


        /// <summary>
        /// Удалить заказ
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
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