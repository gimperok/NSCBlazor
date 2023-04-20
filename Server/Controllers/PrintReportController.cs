using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;
using NSCBlazor.Server.Reports;
using NSCBlazor.Shared.Models;
using ProjectJson.Models;
using System.Net.Http;

namespace NSCBlazor.Server.Controllers
{
    [Route("[controller]")]
    public class PrintReportController : Controller
    {
        private readonly HttpClient httpClient;
        public PrintReportController(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }



        [HttpGet("[action]")]
        public async Task<FileResult> GetResultReqestReportFile(int clientId, int orderId)
        {
            var order = await httpClient.GetFromJsonAsync<OrderMessage>($"{AppSettings.GetApiUrl}{AppSettings.GetOrderById}{orderId}");
            var orderItem = await httpClient.GetFromJsonAsync<List<OrderItemMessage>>($"{AppSettings.GetApiUrl}{AppSettings.GetAllStringsByOrderListId}" + order?.Id);
            var client = await httpClient.GetFromJsonAsync<ClientMessage>($"{AppSettings.GetApiUrl}{AppSettings.GetClientById}" + clientId);

            order.OrderItems = new List<OrderItemMessage>();

            order.ClientId = client.Id;
            order.Client = client;
            order.OrderItems.AddRange(orderItem);

            order.OrderItems.Clear();
            orderItem.ForEach(item => order.OrderItems.Add(item));

            XtraReport report = new OrderReport(order);

            MemoryStream reportStream = new MemoryStream();

            report.ExportToPdf(reportStream);

            if (reportStream.Position > 0)
                reportStream.Position = 0;

            return File(reportStream, "application/pdf", $"Заказ №{order.Id}.pdf");
        }
    }
}
