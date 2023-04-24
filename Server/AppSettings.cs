namespace NSCBlazor.Server
{
    public static class AppSettings
    {
        readonly static IConfiguration configuration;
        static AppSettings()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        public static T GetValueInApiSettingSection<T>(string key)
            => configuration.GetSection("ApiSetting").GetValue<T>(key);

                public static string GetApiUrl
                => GetValueInApiSettingSection<string>("ApiURL");



        public static T GetFromClientsSection<T>(string key)
            => configuration.GetSection("ApiSetting").GetSection("Clients").GetValue<T>(key);

                public static string GetAllClients
                => GetFromClientsSection<string>("GetAllClients");
                public static string GetClientById
                => GetFromClientsSection<string>("GetClientById");
                public static string AddClient
                => GetFromClientsSection<string>("AddClient");
                public static string EditClient
                => GetFromClientsSection<string>("EditClient"); 
                public static string DeleteClientById
                => GetFromClientsSection<string>("DeleteClientById");



        public static T GetFromOrderListsSection<T>(string key)
            => configuration.GetSection("ApiSetting").GetSection("OrderLists").GetValue<T>(key);

                public static string GetOrderById
                => GetFromOrderListsSection<string>("GetOrderById");
                public static string GetLastCreatedOrderListByUserId
                => GetFromOrderListsSection<string>("GetLastCreatedOrderListByUserId");
                public static string GetAllOrderListsByUserId
                => GetFromOrderListsSection<string>("GetAllOrderListsByUserId");
                public static string GetAllOrdersFromDb
                => GetFromOrderListsSection<string>("GetAllOrdersFromDb");
                public static string AddOrderList
                => GetFromOrderListsSection<string>("AddOrderList");
                public static string EditOrderList
                => GetFromOrderListsSection<string>("EditOrderList");
                public static string DeleteOrderListById
                => GetFromOrderListsSection<string>("DeleteOrderListById");



        public static T GetFromOrderStringsSection<T>(string key)
            => configuration.GetSection("ApiSetting").GetSection("OrderStrings").GetValue<T>(key);

                public static string GetAllStringsByOrderListId
                => GetFromOrderStringsSection<string>("GetAllStringsByOrderListId");
                public static string AddOrderString
                => GetFromOrderStringsSection<string>("AddOrderString");
                public static string EditOrderString
                => GetFromOrderStringsSection<string>("EditOrderString");
                public static string DeleteOrderStringById
                => GetFromOrderStringsSection<string>("DeleteOrderStringById");
                public static string DeleteAllStringsForOrder
                => GetFromOrderStringsSection<string>("DeleteAllStringsForOrder");        
    }
}