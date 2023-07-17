using ProjectJson.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class OrderDb 
    {
        /// <summary>
        /// ид заказа
        /// </summary>
        public int Id { get; set; }


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
        public virtual ClientDb? Client { get; set; }


        public virtual List<OrderItemDb>? OrderItems { get; set; } /*= new();*/
    }    
}