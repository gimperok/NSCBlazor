using ProjectJson.Interfaces;
using ProjectJson.Models;


namespace NSCBlazor.Shared.Models
{
    /// <summary>
    /// Класс описывающий обьект заказа
    /// </summary>
    public class Order : OrderMessage
    {

        /// <summary>
        /// Всего позиций в заказе
        /// </summary>
        public int TotalPosition => OrderItems?.Count ?? 0;

        /// <summary>
        /// Всего пар в заказе
        /// </summary>
        public int TotalPairs => OrderItems?.Sum(ordStr => ordStr?.TotalCountPairs ?? 0) ?? 0;

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public double OrderTotalMoney => OrderItems?.Sum(ordStr => ordStr.TotalPrice) ?? 0.00;

        /// <summary>
        /// Депозит за данный заказ
        /// </summary>
        public double Deposit => OrderTotalMoney * 0.3;


        public List<OrderItem>? OrderItems { get; set; } /*= new();*/

        public Client? Client { get; set; }

    }
}