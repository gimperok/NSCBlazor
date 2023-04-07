using ProjectJson.Interfaces;
using ProjectJson.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSCBlazor.Shared.Models
{
    /// <summary>
    /// Класс описывающий клиента
    /// </summary>
    public class Client: IClientMessage
    {
        /// <summary>
        /// id клиента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Страна клиента
        /// </summary>
        public string? Country { get; set; }

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

        /// <summary>
        /// список заказов
        /// </summary>
        public List<OrderList>? OrderLists { get; set; } = new();

        public string FullName => $"{Name} {Surname}";


        //ЗАКОМЕНТИРОВАНО

        ///// <summary>
        ///// внешний ключ
        ///// </summary>
        //public int? OrderListsId { get; set; }

        ///// <summary>
        ///// навигационное свойство
        ///// </summary>
        //[ForeignKey(nameof(OrderListsId))]
        //public OrderList? OrderList { get; set; }

    }
}
