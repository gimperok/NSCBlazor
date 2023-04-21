using System;
using DevExpress.XtraReports.UI;
using ProjectJson.Models;

namespace NSCBlazor.Server.Reports
{
    public partial class OrderReport
    {
        OrderMessage currentOrder;
        public OrderReport(OrderMessage order)
        {
            currentOrder = order;

            InitializeComponent();

            if (currentOrder != null)
                DataSource = currentOrder;

            // В конструкторе в свойствах установить поле DataSource: Обьект > ...;
            // Так же необходимо заполнить поле DataMember;
        }

        private void tableCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (sender is XRTableCell && !string.IsNullOrEmpty((sender as XRTableCell).Text) && ((sender as XRTableCell).Text == "0" || (sender as XRTableCell).Text == "0,0$"))
                (sender as XRTableCell).Text = string.Empty;
        }
    }
}
