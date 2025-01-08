namespace User_login.Contexts
{
    public class AppConfiguration
    {
        private readonly string _connectionString;

        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            _connectionString = root.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;



        }
    }
}