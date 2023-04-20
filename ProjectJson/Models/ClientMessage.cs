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

        [Required(ErrorMessage = "* Обязательно для заполнения")]
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? Name { get; set; }

        [Required(ErrorMessage = "* Обязательно для заполнения")]
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string? Surname { get; set; }

        [Required(ErrorMessage = "* Обязательно для заполнения")]
        /// <summary>
        /// Страна клиента
        /// </summary>
        public string? Country { get; set; }

        [Required(ErrorMessage = "* Обязательно для заполнения")]
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

        public string FullName => $"{Name} {Surname}";

        public DateTime DateRegistration { get; set; } = DateTime.Now;


        ///// <summary>
        ///// список заказов
        ///// </summary>
        //public List<OrderMessage>? Orders { get; set; }


    }
}
