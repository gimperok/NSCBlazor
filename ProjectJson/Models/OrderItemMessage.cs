﻿using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectJson.Models
{
    ///<inheritdoc cref="IOrderItemMessage"/>
    public class OrderItemMessage :IOrderItemMessage
    {
        /// <summary>
        /// id заказа
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "* Обязательно для заполнения")]
        /// <summary>
        /// Код модели
        /// </summary>
        public string? Kod { get; set; }

        [Required(ErrorMessage = "* Обязательно для заполнения")]
        /// <summary>
        /// Кожа модели
        /// </summary>
        public string? Leather { get; set; }

        /// <summary>
        /// Цвет модели
        /// </summary>
        public string? Color { get; set; }
        /// <summary>
        /// 35ый размер
        /// </summary>
        public int Size35 { get; set; } = 0;

        /// <summary>
        /// 36ой размер
        /// </summary>
        public int Size36 { get; set; } = 0;

        /// <summary>                   
        /// 37ой размер                 
        /// </summary>                  
        public int Size37 { get; set; } = 0;
                                        
        /// <summary>                   
        /// 38ой размер                 
        /// </summary>                  
        public int Size38 { get; set; } = 0;
                                        
        /// <summary>                   
        /// 39ый размер                 
        /// </summary>                  
        public int Size39 { get; set; } = 0;
                                        
        /// <summary>                   
        /// 40ой размер                 
        /// </summary>                  
        public int Size40 { get; set; } = 0;
                                        
        /// <summary>                   
        /// 41ый размер                 
        /// </summary>                  
        public int Size41 { get; set; } = 0;

        /// <summary>
        /// Цена одной позиции
        /// </summary>
        public double Price { get; set; } = 0.00;

        /// <summary>
        /// Заметка
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// внешний ключ для OrderList
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Навигационное свойство лист заказа
        /// </summary> 
        [ForeignKey(nameof(OrderId))]
        public OrderMessage? Order { get; set; }
    }
}