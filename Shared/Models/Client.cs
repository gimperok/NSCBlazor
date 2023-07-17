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

        public string FullName => $"{Name} {Surname}";

        /// <summary>
        /// список заказов
        /// </summary>
        public List<Order>? Orders { get; set; }

    }
}
