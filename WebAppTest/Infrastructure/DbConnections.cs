namespace WebAppTest.Infrastructure
{
    public class DbConnections
    {
        public List<DbConnection> Connections { get; set; }
    }

    public class DbConnection
    {
        public string ServerName { get; set; }
        public string ConnectionString { get; set; }
    }
}
