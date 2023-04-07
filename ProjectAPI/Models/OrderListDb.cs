using ProjectAPI.DBContext;
using ProjectJson.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class OrderListDb : IOrderListMessage
    {

        /// <summary>
        /// ид заказа
        /// </summary>
        public int Id { get; set; }

        ///// <summary>
        ///// Всего позиций в заказе
        ///// </summary>
        //public int? TotalPosition 
        //{
        //    get { return TotalPosition; }

        //    set
        //    {
        //        if (OrderStrings != null && OrderStrings.Count > 0)
        //        {
        //            int orderPosition = 0;
        //            foreach (OrderStringDb ordStr in OrderStrings)
        //            {
        //                orderPosition++;
        //            }
        //            value = orderPosition;
        //        }
        //        else value = 0;
        //    }

        //}

        ///// <summary>
        ///// Всего пар в заказе
        ///// </summary>
        //public int? TotalPairs
        //{
        //    get { return TotalPairs; }

        //    set
        //    {
        //        if (OrderStrings != null && OrderStrings.Count > 0)
        //        {
        //            int? pairs = 0;
        //            foreach (OrderStringDb ordStr in OrderStrings)
        //            {
        //                pairs += ordStr.TotalCountPairs;
        //            }
        //            value = pairs;
        //        }
        //        else value =  0;
        //    }
        //}

        ///// <summary>
        ///// Общая сумма заказа
        ///// </summary>
        //public double? OrderTotalMoney
        //{
        //    get { return OrderTotalMoney; }

        //    set
        //    {
        //        if (OrderStrings != null && OrderStrings.Count > 0)
        //        {
        //            double? orderMoney = 0;
        //            foreach (OrderStringDb ordStr in OrderStrings)
        //            {
        //                orderMoney += ordStr.TotalPrice;
        //            }
        //            value = orderMoney;
        //        }
        //        else value = 0.00;
        //    }
        //}

        ///// <summary>
        ///// Депозит за данный заказ
        ///// </summary>
        //public double? Deposit
        //{
        //    get { return Deposit; }
        //    set
        //    {
        //        if (OrderTotalMoney != null && OrderTotalMoney != 0)
        //        {
        //            value = OrderTotalMoney * 0.3;
        //        }
        //        else value = 0.00;
        //    }

        //}












        /// <summary>
        /// Всего позиций в заказе
        /// </summary>
        public int? TotalPosition
        {
            get
            {
                if (OrderStrings != null && OrderStrings.Count > 0)
                {
                    int orderPosition = 0;
                    foreach (OrderStringDb ordStr in OrderStrings)
                    {
                        orderPosition++;
                    }
                    return orderPosition;
                }
                else return 0;
            }
        }


        /// <summary>
        /// Всего пар в заказе
        /// </summary>
        public int? TotalPairs
        {
            get
            {
                if (OrderStrings != null && OrderStrings.Count > 0)
                {
                    int? pairs = 0;
                    foreach (OrderStringDb ordStr in OrderStrings)
                    {
                        pairs += ordStr.TotalCountPairs;
                    }
                    return pairs;
                }
                else return 0;
            }
        }

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public double? OrderTotalMoney
        {
            get
            {
                if (OrderStrings != null && OrderStrings.Count > 0)
                {
                    double? orderMoney = 0;
                    foreach (OrderStringDb ordStr in OrderStrings)
                    {
                        orderMoney += ordStr.TotalPrice;
                    }
                    return orderMoney;
                }
                else return 0.00;
            }
        }

        /// <summary>
        /// Депозит за данный заказ
        /// </summary>
        public double? Deposit
        {
            get
            {
                if (OrderTotalMoney != null && OrderTotalMoney != 0)
                {
                    return OrderTotalMoney * 0.3;
                }
                else return 0.00;
            }
        }




























        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime? DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата изменения заказа
        /// </summary>
        public DateTime? DateModify { get; set; }

        /// <summary>
        /// Список позиций в заказе
        /// </summary>
        public List<OrderStringDb>? OrderStrings { get; set; } = new();


        /// <summary>
        /// внешний ключ Client
        /// </summary>
        public int? ClientId { get; set; }

        ///// <summary>
        ///// Навигационное свойство клиент
        ///// </summary>
        //[ForeignKey(nameof(ClientId))]
        //public ClientDb? Client { get; set; }
    }
}
