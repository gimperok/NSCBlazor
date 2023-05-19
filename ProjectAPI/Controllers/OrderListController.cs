using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Services.Repository.Interfaces;
using ProjectJson.Models;
using System.Net.Http;
using System.Text.Json;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderListController : ControllerBase
    {

        private readonly IOrderRepository orderRepository;

        public OrderListController(IOrderRepository _repository)
        {
            orderRepository = _repository;
        }


        /// <summary>
        /// Получить заказ по его ID номеру
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Заказ по его ID номеру</returns>
        [HttpGet]
        public async Task<OrderMessage> GetOrderById(int id)
        {
            return orderRepository.GetById(id);
        }

        /// <summary>
        /// Получить последний созданный заказ по ID клиента
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Последний созданный заказ по ID клиента</returns>
        [HttpGet]
        public OrderMessage GetLastCreatedOrderListByUserId(int id)
        {
            return orderRepository.GetLastCreatedOrderByClientId(id);
        }

        /// <summary>
        /// Получить список заказов клиента по его ID
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Cписок заказов клиента по его ID</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrderListsByUserId(int clientId)
        {
            return orderRepository.GetAllOrdersByClientId(clientId);
        }

        /// <summary>
        /// Получить список всех заказов из бд
        /// </summary>
        /// <returns>Cписок всех заказов</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrdersFromDb()
        {
            return orderRepository.GetList();
        }

        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="orderList">Обьект заказа</param>
        /// <returns></returns>
        [HttpPost]
        public bool AddOrder(OrderMessage orderList)
        {
            if (!ModelState.IsValid)
                return false;
            return orderRepository.Add(orderList);
        }

        /// <summary>
        /// Изменение заказа
        /// </summary>
        /// <param name="orderList">Обьект заказа</param>
        /// <returns></returns>
        [HttpPut]
        public bool EditOrder(OrderMessage orderList)
        {
            if (!ModelState.IsValid)
                return false;
            return orderRepository.Edit(orderList);
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns></returns>
        [HttpDelete]
        public bool DeleteOrderListById(int id)
        {
            return orderRepository.Delete(id);
        }
    }
}