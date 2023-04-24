using ProjectJson.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson.Interfaces
{
    public interface IOrderMessage
    {
        /// <summary>
        /// ид заказа
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime? DateCreate { get; set; }

        /// <summary>
        /// Дата изменения заказа
        /// </summary>
        public DateTime? DateModify { get; set; }


        /// <summary>
        /// внешний ключ Client
        /// </summary>
        public int? ClientId { get; set; }
    }
}
