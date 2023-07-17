using NSCBlazor.Shared.Models;
using ProjectJson.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NSCBlazor.Client.Helpers
{
    public static class RouteHelper
    {

        //POST
        public static async Task<int?> PostObject<T>(this HttpClient Http, T obj)
        {
            HttpResponseMessage? insertResponse = null;

            switch (typeof(T).Name)
            {
                case nameof(NSCBlazor.Shared.Models.Client):
                    insertResponse = await Http.PostAsJsonAsync("/Client/AddClient", obj);
                    break;

                case nameof(Order):
                    insertResponse = await Http.PostAsJsonAsync("/Order/AddOrderList", obj);
                    break;

                case nameof(OrderItem):
                    insertResponse = await Http.PostAsJsonAsync("/OrderItem/AddOrderString", obj);
                    break;
            }

            int? returnId = insertResponse != null ? await WebServiceHelper.GetContentFromResponse<int?>(insertResponse) : null;
            return returnId;
        }


        //PUT
        public static async Task<bool> PutObject<T>(this HttpClient Http, T obj)
        {
            HttpResponseMessage? updateResponse = null;

            switch (typeof(T).Name)
            {
                case nameof(NSCBlazor.Shared.Models.Client):
                    updateResponse = await Http.PutAsJsonAsync("/Client/EditClient", obj);
                    break;

                case nameof(Order):
                    updateResponse = await Http.PutAsJsonAsync("/Order/EditOrderList", obj);
                    break;

                case nameof(OrderItem):
                    updateResponse = await Http.PutAsJsonAsync("/OrderItem/EditOrderString", obj);
                    break;
            }

            bool resultUpdate = updateResponse != null ? await WebServiceHelper.GetContentFromResponse<bool>(updateResponse) : false;
            return resultUpdate;
        }


    #region OrderMethods
        //GET
        public static async Task<Order> GetOrderById(this HttpClient Http, int orderId)
        {
            return await Http.GetFromJsonAsync<Order>($"/Order/GetOrderById?id={orderId}");
        }

        public static async Task<List<Order>> GetAllOrdersFromDB(this HttpClient Http)
        {
            var allOrdersFromDb = await Http.GetFromJsonAsync<List<Order>>("/Order/GetAllOrdersFromDb");

            if(allOrdersFromDb?.Count > 0)
            {
                foreach (var order in allOrdersFromDb)
                {
                    order.OrderItems = await GetAllStringsByOrderListId(Http, order.Id);
                }
            }
            return allOrdersFromDb;
        }

        public static async Task<List<Order>> GetAllOrderByUserId(this HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<List<Order>>("/Order/GetAllOrderListsByUserId?clientId=" + clientId);
        }

        [Obsolete]
        public static async Task<Order> GetLastCreatedOrderListByClientId(this HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<Order>($"/Order/GetLastCreatedOrderListByUserId?id={clientId}");
        }


        //DELETE
        public static async Task<bool> DeleteOrderWithStrings(this HttpClient Http, int id)
        {
            var response = await Http.DeleteAsync($"/OrderItem/DeleteAllStringsForOrder?id={id}");
            bool delAllStringsResult = await WebServiceHelper.GetContentFromResponse<bool>(response);


            if (delAllStringsResult)
            {
                var deleteOrderResponse = await Http.DeleteAsync($"/Order/DeleteOrderList?id={id}");
                return await WebServiceHelper.GetContentFromResponse<bool>(deleteOrderResponse);
            }
            return false;
        }
        #endregion


        #region OrderItemMethods
        //GET
        public static async Task<List<OrderItem>> GetAllStringsByOrderListId(this HttpClient Http, int orderId)
        {
            return await Http.GetFromJsonAsync<List<OrderItem>>($"/OrderItem/GetAllStringsByOrderListId?id={orderId}");
        }

        //DELETE
        public static async Task<bool> DeleteOrderString(this HttpClient Http, int id)
        {
            var deleteResponse = await Http.DeleteAsync($"/OrderItem/DeleteOrderString?id={id}");

            return await WebServiceHelper.GetContentFromResponse<bool>(deleteResponse);
        }
        #endregion


        #region ClientMethods
        //GET
        public static async Task<NSCBlazor.Shared.Models.Client> GetClientById(this HttpClient Http, int clientId)
        {
            return await Http.GetFromJsonAsync<NSCBlazor.Shared.Models.Client>($"/Client/GetClientById?id={clientId}");
        }

        public static async Task<List<NSCBlazor.Shared.Models.Client>> GetAllClients(this HttpClient Http)
        {
            return await Http.GetFromJsonAsync<List<NSCBlazor.Shared.Models.Client>>("/Client/GetAllClients");
        }
        

        //DELETE
        public static async Task<bool> DeleteClient(this HttpClient Http, int clientId)
        {
            var response = await Http.DeleteAsync("/Client/DeleteClient?id=" + clientId);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion
    }
}