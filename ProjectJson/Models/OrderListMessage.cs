using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectJson.Models
{
    ///<inheritdoc cref="IOrderListMessage"/>
    public class OrderListMessage :IOrderListMessage
    {
        /// <summary>
        /// ид заказа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Всего позиций в заказе
        /// </summary>
        public int? TotalPosition { get;}

        /// <summary>
        /// Всего пар в заказе
        /// </summary>
        public int? TotalPairs { get;}

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public double? OrderTotalMoney { get;}

        /// <summary>
        /// Депозит за данный заказ
        /// </summary>
        public double? Deposit { get;}

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime? DateCreate { get; set; }
        /// <summary>
        /// Дата изменения заказа
        /// </summary>
        public DateTime? DateModify { get; set; }

        /// <summary>
        /// внешний ключ Client
        /// </summary>
        public int? ClientId { get; set; }

        ///// <summary>
        ///// Навигационное свойство клиент
        ///// </summary>
        //[ForeignKey(nameof(ClientId))]
        //public ClientMessage? Client { get; set; }

    }
}
