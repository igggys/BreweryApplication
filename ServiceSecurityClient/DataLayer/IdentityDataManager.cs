using ServiceSecurityServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ServiceSecurityServer.DataLayer
{
    public class IdentityDataManager
    {
        private readonly string _connectionString;
        public IdentityDataManager(IdentityDataManagerSettingsReader identityDataManagerSettingsReader) 
        {
            _connectionString = identityDataManagerSettingsReader.IdentityDataManagerSettingsRead()?.ConnectionString;
        }

        public async Task<Guid> GetSessionIdAsync(string fromServiceName, Guid fromServiceId, string toServiceName, Guid toServiceId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@FromServiceName", fromServiceName);
                    command.Parameters.AddWithValue("@FromServiceId", fromServiceId);
                    command.Parameters.AddWithValue("@ToServiceName", toServiceName);
                    command.Parameters.AddWithValue("@ToServiceId", toServiceId);

                    await connection.OpenAsync();
                    var sessionIdObject = await command.ExecuteScalarAsync();
                    return Guid.Parse(sessionIdObject.ToString());
                }
            }
        }

        public Guid GetSessionId(string fromServiceName, Guid fromServiceId, string toServiceName, Guid toServiceId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@FromServiceName", fromServiceName);
                    command.Parameters.AddWithValue("@FromServiceId", fromServiceId);
                    command.Parameters.AddWithValue("@ToServiceName", toServiceName);
                    command.Parameters.AddWithValue("@ToServiceId", toServiceId);

                    connection.Open();
                    var sessionIdObject = command.ExecuteScalar();
                    return Guid.Parse(sessionIdObject.ToString());
                }
            }
        }
    }
}
