using NSCBlazor.Shared.Models;
using ProjectJson.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace NSCBlazor.Client.Helpers
{
    public static class RouteHelper
    {
        #region OrderMethods
        //GET
        public static async Task<Order> GetOrderById(HttpClient Http, int orderId)
        {
            return await Http.GetFromJsonAsync<Order>($"https://localhost:7256/Order/GetOrderById?id={orderId}");
        }

        public static async Task<List<Order>> GetAllOrdersFromDB(HttpClient Http)
        {
            var allOrdersFromDb = await Http.GetFromJsonAsync<List<Order>>("https://localhost:7256/Order/GetAllOrdersFromDb");

            if(allOrdersFromDb?.Count > 0)
            {
                foreach (var order in allOrdersFromDb)
                {
                    order.OrderItems = await GetAllStringsByOrderListId(Http, order.Id);
                }
            }
            return allOrdersFromDb;
        }

        public static async Task<List<Order>> GetAllOrderByUserId(HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<List<Order>>("https://localhost:7256/Order/GetAllOrderListsByUserId?clientId=" + clientId);
        }

        public static async Task<Order> GetLastCreatedOrderListByClientId(HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<Order>($"https://localhost:7256/Order/GetLastCreatedOrderListByUserId?id={clientId}");
        }


        //POST
        public static async Task<HttpResponseMessage> AddOrder(HttpClient Http, Order order)
        {
            return await Http.PostAsJsonAsync("https://localhost:7256/Order/AddOrderList", order);
        }

        //DELETE
        public static async Task DeleteOrderWithStrings(HttpClient Http, int id)
        {
            var response = await Http.DeleteAsync($"https://localhost:7256/OrderItem/DeleteAllStringsForOrder?id={id}");
            if (response.IsSuccessStatusCode)
                await DeleteOrder(Http, id);
        }

        public static async Task DeleteOrder(HttpClient Http, int id)
        {
            await Http.DeleteAsync($"https://localhost:7256/Order/DeleteOrderList?id={id}");
        }
        #endregion


        #region OrderItemMethods
        //GET
        public static async Task<List<OrderItem>> GetAllStringsByOrderListId(HttpClient Http, int orderId)
        {
            return await Http.GetFromJsonAsync<List<OrderItem>>($"https://localhost:7256/OrderItem/GetAllStringsByOrderListId?id={orderId}");
        }

        //POST
        public static async Task<HttpResponseMessage> AddOrderString(HttpClient Http, OrderItem orderString)
        {
            return await Http.PostAsJsonAsync("https://localhost:7256/OrderItem/AddOrderString", orderString);
        }

        //DELETE
        public static async Task<HttpResponseMessage> DeleteOrderString(HttpClient Http, OrderItem orderString)
        {
            return await Http.DeleteAsync($"https://localhost:7256/OrderItem/DeleteOrderString?id={orderString.Id}");
        }
        #endregion


        #region ClientMethods
        //GET
        public static async Task<NSCBlazor.Shared.Models.Client> GetClientById(HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<NSCBlazor.Shared.Models.Client>($"https://localhost:7256/Client/GetClientById?id={clientId}");
        }

        public static async Task<List<NSCBlazor.Shared.Models.Client>> GetAllClients(HttpClient Http)
        {
            return await Http.GetFromJsonAsync<List<NSCBlazor.Shared.Models.Client>>("https://localhost:7256/Client/GetAllClients");
        }
        
        //POST
        public static async Task<HttpResponseMessage> AddClient(HttpClient Http, NSCBlazor.Shared.Models.Client client)
        {
            return await Http.PostAsJsonAsync("https://localhost:7256/Client/AddClient", client);
        }

        //PUT
        public static async Task<HttpResponseMessage> EditClient(HttpClient Http, NSCBlazor.Shared.Models.Client client)
        {
            return await Http.PutAsJsonAsync("https://localhost:7256/Client/EditClient", client);
        }

        //DELETE
        public static async Task<bool> DeleteClient(HttpClient Http, int clientId)
        {
            var response = await Http.DeleteAsync("https://localhost:7256/Client/DeleteClient?id=" + clientId);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion
    }
}