using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectJson.Models
{
    ///<inheritdoc cref="IClientMessage"/>
    public class ClientMessage: IClientMessage
    {
        [Required]
        /// <summary>
        /// id клиента
        /// </summary>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? Name { get; set; }

        [Required]
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string? Surname { get; set; }

        [Required]
        /// <summary>
        /// Страна клиента
        /// </summary>
        public string? Country { get; set; }

        [Required]
        /// <summary>
        /// Город клиента
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Транспортная компания клиента
        /// </summary>
        public string? Cargo { get; set; }

        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string? Tel { get; set; }

        ///// <summary>
        ///// список заказов
        ///// </summary>
        //public List<OrderListMessage>? OrderLists { get; set; }
    }
}
