﻿using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Получить все строки заказа по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Список строк указанного заказа</returns>
        [HttpGet]
        public async Task<List<OrderItemMessage>> GetAllStringsByOrderListId(int id)
        {
            List<OrderItemMessage> orderStrings = await httpClient.GetFromJsonAsync<List<OrderItemMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllStringsByOrderListId}" + id);
            return orderStrings;
        }

        /// <summary>
        /// Добавить строку в заказ
        /// </summary>
        /// <param name="orderString">Обьект строки</param>
        [HttpPost]
        public async Task<bool> AddOrderString(OrderItemMessage orderString)
        {
            var response = await httpClient.PostAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.AddOrderString}", orderString);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        /// <summary>
        /// Изменить обьект строки
        /// </summary>
        /// <param name="orderString">Обьект строки</param>
        [HttpPut]
        public async Task<bool> EditOrderString(OrderItemMessage orderString)
        {
            var response = await httpClient.PutAsJsonAsync($"{AppSettings.GetApiUrl}{AppSettings.EditOrderString}", orderString);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        /// <summary>
        /// Удалить обьект строки по ее ID
        /// </summary>
        /// <param name="id">Идентификатор строки</param>
        [HttpDelete]
        public async Task<bool> DeleteOrderString(int id)
        {
            var response = await httpClient.DeleteAsync($"{AppSettings.GetApiUrl}{AppSettings.DeleteOrderStringById}" + id);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        /// <summary>
        /// Удалить все строки по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
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
