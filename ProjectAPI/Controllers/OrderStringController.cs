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


        [HttpGet]
        public List<OrderItemMessage> GetAllStringsByOrderListId(int orderId)
        {
            List<OrderItemMessage> orderStringsList = new List<OrderItemMessage>();
            if (db.OrderItems.Any(p => p.OrderId == orderId))
                orderStringsList = db.OrderItems.Where(p => p.OrderId == orderId).ToList();
            return orderStringsList;
        }


        [HttpPost]
        public bool AddOrderString(OrderItemMessage orderString)
        {
            if (!ModelState.IsValid)
                return false;

            db.OrderItems.Add(orderString);
            db.SaveChanges();
            return true;
        }

        [HttpPut]
        public bool EditOrderString(OrderItemMessage editedOrderString)
        {
            if (!ModelState.IsValid)
                return false;

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
            db.SaveChanges();
            return true;
        }


        [HttpDelete]
        public bool DeleteOrderStringById(int id)
        {
            var orderString = db.OrderItems.FirstOrDefault(p => p.Id == id);
            if (orderString == null)
                return false;

            db.OrderItems.Remove(orderString);
            db.SaveChanges();
            return true;
        }

        [HttpDelete]
        public bool DeleteAllStringsForOrder(int id)
        {
            var stringList = db.OrderItems.Where(p => p.OrderId == id).ToList();

            if(stringList is not null && stringList.Count !=0)
            {
                foreach(var str in stringList)
                {
                    db.OrderItems.Remove(str);
                }
            }

            db.SaveChanges();
            return true;
        }
    }
}