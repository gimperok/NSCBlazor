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

            // � ������������ � ��������� ���������� ���� DataSource: ������ > ...;
            // ��� �� ���������� ��������� ���� DataMember;
        }
    }
}
