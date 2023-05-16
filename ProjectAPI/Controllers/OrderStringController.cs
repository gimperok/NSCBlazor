using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Models;
using ProjectJson.Models;
using System.Text.Json;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStringController : ControllerBase
    {
        private readonly ApplicationContext db;

        public OrderStringController(ApplicationContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Получить все строки заказа по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Список строк указанного заказа</returns>
        [HttpGet]
        public List<OrderItemMessage> GetAllStringsByOrderListId(int orderId)
        {
            List<OrderItemMessage> orderStringsList = new List<OrderItemMessage>();
            try
            {
                if (db.OrderItems.Any(p => p.OrderId == orderId))
                    orderStringsList = db.OrderItems.Where(p => p.OrderId == orderId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка получения списка из бд. Место: OrderStringController. Error text:{e.Message}");
            }           
            return orderStringsList;
        }

        /// <summary>
        /// Добавить строку в заказ
        /// </summary>
        /// <param name="orderString">Обьект строки</param>
        [HttpPost]
        public bool AddOrderString(OrderItemMessage orderString)
        {
            if (!ModelState.IsValid)
                return false;

            try
            {
                db.OrderItems.Add(orderString);

                var order = db.Orders.Where(x => x.Id == orderString.OrderId).FirstOrDefault();
                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка добавления обьекта в бд. Место: OrderStringController. Error text:{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Изменить обьект строки
        /// </summary>
        /// <param name="orderString">Обьект строки</param>
        [HttpPut]
        public bool EditOrderString(OrderItemMessage editedOrderString)
        {
            if (!ModelState.IsValid)
                return false;

            try
            {
                var currentOrderString = db.OrderItems.FirstOrDefault(p => p.Id == editedOrderString.Id);

                if (currentOrderString == null)
                    return false;

                currentOrderString.Kod = editedOrderString.Kod;
                currentOrderString.Leather = editedOrderString.Leather;
                currentOrderString.Color = editedOrderString.Color;

                currentOrderString.Size35 = editedOrderString.Size35;
                currentOrderString.Size36 = editedOrderString.Size36;
                currentOrderString.Size37 = editedOrderString.Size37;
                currentOrderString.Size38 = editedOrderString.Size38;
                currentOrderString.Size39 = editedOrderString.Size39;
                currentOrderString.Size40 = editedOrderString.Size40;
                currentOrderString.Size41 = editedOrderString.Size41;

                currentOrderString.Price = editedOrderString.Price;
                currentOrderString.Note = editedOrderString.Note;


                db.OrderItems.Update(currentOrderString);

                var order = db.Orders.Where(x => x.Id == currentOrderString.OrderId).FirstOrDefault();
                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка изменения обьекта в бд. Место: OrderStringController. Error text:{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Удалить обьект строки по ее ID
        /// </summary>
        /// <param name="id">Идентификатор строки</param>
        [HttpDelete]
        public bool DeleteOrderStringById(int id)
        {
            try
            {
                var orderString = db.OrderItems.FirstOrDefault(p => p.Id == id);
                if (orderString == null)
                    return false;

                db.OrderItems.Remove(orderString);

                var order = db.Orders.Where(x => x.Id == orderString.OrderId).FirstOrDefault();
                order.DateModify = DateTime.Now;

                db.Orders.Update(order);

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд. Место: OrderStringController. Error text:{e.Message}");
                return false;
            }

        }

        /// <summary>
        /// Удалить все строки по ID заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpDelete]
        public bool DeleteAllStringsForOrder(int id)
        {
            try
            {
                var stringList = db.OrderItems.Where(p => p.OrderId == id).ToList();

                if (stringList is not null && stringList.Count != 0)
                {
                    foreach (var str in stringList)
                    {
                        db.OrderItems.Remove(str);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка удаления обьекта из бд. Место: OrderStringController. Error text:{e.Message}");
                return false;
            }
        }
    }
}