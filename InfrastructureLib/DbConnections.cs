using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLib
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
