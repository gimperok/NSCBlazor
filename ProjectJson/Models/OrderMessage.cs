using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectJson.Models
{
    ///<inheritdoc cref="IOrderMessage"/>
    public class OrderMessage :IOrderMessage
    {
        /// <summary>
        /// ид заказа
        /// </summary>
        public int Id { get; set; }

        ///// <summary>
        ///// Всего позиций в заказе
        ///// </summary>
        //public int TotalPosition => OrderItems?.Count ?? 0;

        ///// <summary>
        ///// Всего пар в заказе
        ///// </summary>
        //public int TotalPairs => OrderItems?.Sum(ordStr => ordStr?.TotalCountPairs ?? 0) ?? 0;

        ///// <summary>
        ///// Общая сумма заказа
        ///// </summary>
        //public double OrderTotalMoney => OrderItems?.Sum(ordStr => ordStr.TotalPrice) ?? 0.00;

        ///// <summary>
        ///// Депозит за данный заказ
        ///// </summary>
        //public double Deposit => OrderTotalMoney * 0.3;

        public List<OrderItemMessage>? OrderItems { get; set; } /*= new();*/


        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime? DateCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Дата изменения заказа
        /// </summary>
        public DateTime? DateModify { get; set; }

        /// <summary>
        /// внешний ключ Client
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// Навигационное свойство клиент
        /// </summary>
        [ForeignKey(nameof(ClientId))]
        public ClientMessage? Client { get; set; }

    }
}
