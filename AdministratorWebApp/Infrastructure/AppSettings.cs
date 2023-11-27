namespace AdministratorWebApp.Infrastructure
{
    public class AppSettings
    {
        public List<MSSqlConnection> MSSqlConnections { get; set; }
    }

    public class MSSqlConnection
    {
        public string ConnectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
