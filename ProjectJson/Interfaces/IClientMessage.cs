using ProjectJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson.Interfaces
{
    public interface IClientMessage
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

        //public List<OrderListMessage>? OrderLists { get; set; }

    }
}
