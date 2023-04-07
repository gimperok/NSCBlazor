using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Models;
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
        public List<OrderStringDb> GetAllStringsByOrderListId(int orderId)
        {
            List<OrderStringDb> orderStringsList = new List<OrderStringDb>();
            if (db.OrderStrings.Any(p => p.OrderListId == orderId))
                orderStringsList = db.OrderStrings.Where(p => p.OrderListId == orderId).ToList();
            return orderStringsList;

            //if (orderStringsList.Count() == 0)
            //    return String.Empty;

            //return JsonSerializer.Serialize(orderStringsList);
        }


        [HttpPost]
        public bool AddOrderString(OrderStringDb orderString)
        {
            if (!ModelState.IsValid)
                return false;

            db.OrderStrings.Add(orderString);
            db.SaveChanges();
            return true;
        }

        [HttpPut]
        public bool EditOrderString(OrderStringDb editedOrderString)
        {
            if (!ModelState.IsValid)
                return false;

            var currentOrderString = db.OrderStrings.FirstOrDefault(p => p.Id == editedOrderString.Id);

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


            db.OrderStrings.Update(currentOrderString);
            db.SaveChanges();
            return true;
        }


        [HttpDelete]
        public bool DeleteOrderStringById(int id)
        {
            var orderString = db.OrderStrings.FirstOrDefault(p => p.Id == id);
            if (orderString == null)
                return false;

            db.OrderStrings.Remove(orderString);
            db.SaveChanges();
            return true;
        }

        [HttpDelete]
        public bool DeleteAllStringsForOrder(int id)
        {
            var stringList = db.OrderStrings.Where(p => p.OrderListId == id).ToList();

            if(stringList is not null && stringList.Count !=0)
            {
                foreach(var str in stringList)
                {
                    db.OrderStrings.Remove(str);
                }
            }

            db.SaveChanges();
            return true;
        }
    }
}
