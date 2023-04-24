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