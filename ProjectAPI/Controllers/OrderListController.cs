using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectJson.Models;
using System.Net.Http;
using System.Text.Json;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderListController : ControllerBase
    {
        private readonly ApplicationContext db;

        public OrderListController(ApplicationContext _db)
        {
            db = _db;
        }


        /// <summary>
        /// Получить заказ по его ID номеру
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Заказ по его ID номеру</returns>
        [HttpGet]
        public async Task<OrderMessage> GetOrderById(int id)
        {
            OrderMessage? orderList = db.Orders.Where(x => x.Id == id).FirstOrDefault();
            return orderList;
        }


        /// <summary>
        /// Получить последний созданный заказ по ID клиента
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Последний созданный заказ по ID клиента</returns>
        [HttpGet]
        public OrderMessage GetLastCreatedOrderListByUserId(int id)
        {
            OrderMessage orderList = new OrderMessage();
            if (db.Orders.Any())
                orderList = db.Orders.Where(x => x.ClientId == id).OrderByDescending(s => s.DateCreate).FirstOrDefault();

            return orderList;
        }


        /// <summary>
        /// Получить список заказов клиента по его ID
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Cписок заказов клиента по его ID</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrderListsByUserId(int clientId)
        {
            List<OrderMessage> orderLists = new List<OrderMessage>();

            if (db.Orders.Any(x => x.ClientId == clientId))
            {
                orderLists = db.Orders.Where(x => x.ClientId == clientId).OrderByDescending(t => t).Select(r => r).ToList();                
            }

            return orderLists;
        }


        /// <summary>
        /// Получить список всех заказов из бд
        /// </summary>
        /// <returns>Cписок всех заказов</returns>
        [HttpGet]
        public async Task<List<OrderMessage>> GetAllOrdersFromDb()
        {
            List<OrderMessage> allOrders = db.Orders.OrderByDescending(x => x.Id).ToList();

            foreach (var order in allOrders)
            {
                order.Client = db.Clients.Where(p => p.Id == order.ClientId).FirstOrDefault();
            }

            return allOrders;
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

            db.Orders.Add(orderList);
            db.SaveChanges();
            return true;
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

            var editOrderList = db.Orders.FirstOrDefault(p => p.Id == orderList.Id);

            if (editOrderList == null)
                return false;

            db.Orders.Update(editOrderList);
            db.SaveChanges();
            return true;
        }


        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns></returns>
        [HttpDelete]
        public bool DeleteOrderListById(int id)
        {
            var orderList = db.Orders.FirstOrDefault(p => p.Id == id);
            if (orderList == null)
                return false;

            db.Orders.Remove(orderList);
            db.SaveChanges();
            return true;
        }
    }
}
