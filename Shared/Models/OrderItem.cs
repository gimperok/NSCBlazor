using ProjectJson.Interfaces;
using ProjectJson.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSCBlazor.Shared.Models
{
    /// <summary>
    /// Класс описывающий обьект строки заказа
    /// </summary>
    public class OrderItem : OrderItemMessage
    {
        /// <summary>
        /// Всего пар в этой строке
        /// </summary>
        public int TotalCountPairs => Size35 + Size36 + Size37 + Size38 + Size39 + Size40 + Size41;

        /// <summary>
        /// Общая цена серии
        /// </summary>
        public double TotalPrice => TotalCountPairs * Price;
    }
}