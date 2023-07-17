using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Models;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;
using System.Text.Json;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStringController : ControllerBase
    {

        private readonly IOrderItemRepository orderItemRepository;

        public OrderStringController(IOrderItemRepository _repository)
        {
            orderItemRepository = _repository;
        }


        /// <summary>
        /// Получить все строки заказа по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Список строк указанного заказа</returns>
        [HttpGet]
        public List<OrderItemMessage> GetAllStringsByOrderListId(int orderId)
        {
            return orderItemRepository.GetListItemsByOrderId(orderId);
        }

        /// <summary>
        /// Добавить строку в заказ
        /// </summary>
        /// <param name="orderString">Обьект строки</param>
        [HttpPost]
        public int AddOrderString(OrderItemMessage orderString)
        {
            if (!ModelState.IsValid)
                return int.MinValue;
            return orderItemRepository.Add(orderString);
        }

        /// <summary>
        /// Изменить обьект строки
        /// </summary>
        /// <param name="editedOrderString">Обьект строки</param>
        [HttpPut]
        public bool EditOrderString(OrderItemMessage editedOrderString)
        {
            if (!ModelState.IsValid)
                return false;
            return orderItemRepository.Edit(editedOrderString);
        }

        /// <summary>
        /// Удалить обьект строки по ее ID
        /// </summary>
        /// <param name="id">Идентификатор строки</param>
        [HttpDelete]
        public bool DeleteOrderStringById(int id)
        {
            return orderItemRepository.Delete(id);
        }

        /// <summary>
        /// Удалить все строки по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpDelete]
        public bool DeleteAllStringsForOrder(int id)
        {
            return orderItemRepository.DeleteListItemsByOrderId(id);
        }
    }
}