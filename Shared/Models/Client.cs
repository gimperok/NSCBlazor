using ProjectJson.Interfaces;
using ProjectJson.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSCBlazor.Shared.Models
{
    /// <summary>
    /// Класс описывающий клиента
    /// </summary>
    public class Client : ClientMessage
    {
        /// <summary>
        /// список заказов
        /// </summary>
        public List<Order>? Orders { get; set; }

    }
}
