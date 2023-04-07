using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DBContext;
using ProjectAPI.Models;
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

        [HttpGet] //ЗАКАЗ ПО ID
        public async Task<OrderListDb> GetOrderById(int id)
        {
            OrderListDb? orderList = db.OrderLists.Where(x => x.Id == id).FirstOrDefault();
            return orderList;
        }



        [HttpGet]
        public OrderListDb GetLastCreatedOrderListByUserId(int id)
        {
            OrderListDb orderList = new OrderListDb();
            if (db.OrderLists.Any())
                orderList = db.OrderLists.Where(x => x.ClientId == id).OrderByDescending(s => s.DateCreate).FirstOrDefault();

            //.Select(t => t).Where(x => x.ClientId == id).OrderByDescending(s => s.DateCreate)

            //foreach(var str in)
            //orderList.OrderStrings = db.OrderStrings.Where(x => x.)
            return orderList;

            //if (OrderList == null)
            //    return String.Empty;

            //return JsonSerializer.Serialize(OrderList);
        }


        [HttpGet]
        public async Task<List<OrderListDb>> GetAllOrderListsByUserId(int clientId)
        {
            List<OrderListDb> orderLists = new List<OrderListDb>();

            if (db.OrderLists.Any(x => x.ClientId == clientId))
            {
                orderLists = db.OrderLists.Where(x => x.ClientId == clientId).OrderByDescending(t => t).Select(r => r).ToList();                
            }

            return orderLists;
        }


        [HttpPost]
        public bool AddOrder(OrderListDb orderList)
        {
            if (!ModelState.IsValid)
                return false;

            db.OrderLists.Add(orderList);
            db.SaveChanges();
            return true;
        }


        [HttpPut]
        public bool EditOrder(OrderListDb orderList)
        {
            if (!ModelState.IsValid)
                return false;

            var editOrderList = db.OrderLists.FirstOrDefault(p => p.Id == orderList.Id);

            if (editOrderList == null)
                return false;

            editOrderList.DateModify = orderList.DateModify;
            editOrderList.OrderStrings = orderList.OrderStrings;

            //foreach(var odrstr in orderList.OrderStrings)
            //{
            //    editOrderList.TotalPosition += odrstr.TotalCountPairs;
            //    editOrderList.TotalPairs += odrstr.TotalCountPairs;

            //    editOrderList.OrderTotalMoney += odrstr.TotalPrice;
            //    editOrderList.OrderTotalMoney += odrstr.TotalPrice;

            //}

            //editOrderList.TotalPosition = orderList.TotalPosition;
            ////editOrderList.TotalPosition = orderList.TotalPosition;
            //editOrderList.TotalPairs = orderList.TotalPairs;
            //editOrderList.OrderTotalMoney = orderList.OrderTotalMoney;
            //editOrderList.Deposit = orderList.Deposit;

            db.OrderLists.Update(editOrderList);
            db.SaveChanges();
            return true;
        }


        [HttpDelete]
        public bool DeleteOrderListById(int id)
        {
            var orderList = db.OrderLists.FirstOrDefault(p => p.Id == id);
            if (orderList == null)
                return false;

            db.OrderLists.Remove(orderList);
            db.SaveChanges();
            return true;
        }
    }
}
