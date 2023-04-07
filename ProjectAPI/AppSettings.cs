namespace ProjectAPI
{
    public static class AppSettings
    {
        readonly static IConfiguration configuration;

        static AppSettings()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        public static string ConnectionString
        {
            get
            {
                var settingsConnectionString = configuration.GetConnectionString("DefaultConnection");
                return settingsConnectionString;
            }
        }
    }
}
