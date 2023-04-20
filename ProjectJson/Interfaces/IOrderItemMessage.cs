using ProjectJson.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson.Interfaces
{
    public interface IOrderItemMessage
    {
        /// <summary>
        /// id заказа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код модели
        /// </summary>
        public string? Kod { get; set; }

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
        public int Size35 { get; set; }

        /// <summary>
        /// 36ой размер
        /// </summary>
        public int Size36 { get; set; }

        /// <summary>
        /// 37ой размер
        /// </summary>
        public int Size37 { get; set; }

        /// <summary>
        /// 38ой размер
        /// </summary>
        public int Size38 { get; set; }

        /// <summary>
        /// 39ый размер
        /// </summary>
        public int Size39 { get; set; }

        /// <summary>
        /// 40ой размер
        /// </summary>
        public int Size40 { get; set; }

        /// <summary>
        /// 41ый размер
        /// </summary>
        public int Size41 { get; set; }

        /// <summary>
        /// Всего пар в этой строке
        /// </summary>
        public int TotalCountPairs { get; }

        /// <summary>
        /// Цена одной позиции
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Общая цена серии
        /// </summary>
        public double TotalPrice { get; }

        /// <summary>
        /// Заметка
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// внешний ключ для OrderList
        /// </summary>
        public int? OrderId { get; set; }
        //public OrderListMessage? OrderList { get; set; }
    }
}
