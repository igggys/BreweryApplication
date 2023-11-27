using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebAppGeoCodingServices.Infrastructure;

namespace WebAppGeoCodingServices.DataLayer
{
    public class SessionsManager
    {
        private readonly string _connectionString;
        private readonly ServiceProperties _thisServiceProperties;
        private readonly List<ServiceProperties> _services;
        public SessionsManager(IOptions<List<DbConnection>> dbConnections, IOptions<ServiceProperties> thisServiceProperties, IOptions<List<ServiceProperties>> services)
        {
            _connectionString = dbConnections.Value.FirstOrDefault(item => item.ServerName == "IdentityServiceDb").ConnectionString;
            _thisServiceProperties = thisServiceProperties.Value;
            _services = services.Value;
        }

        public async Task<Guid?> SessionCreation(string toServiceName)
        {
            var toService = _services.FirstOrDefault(item => item.ServiceName == toServiceName);
            if (toService != null)
            {
                using (SqlConnection connection = new(_connectionString))
                {
                    using (SqlCommand insertCommand = connection.CreateCommand())
                    {
                        insertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        insertCommand.CommandText = "proc_SessionsInsert";
                        insertCommand.Parameters.AddWithValue("@FromServiceName", _thisServiceProperties.ServiceName);
                        insertCommand.Parameters.AddWithValue("@FromServiceId", _thisServiceProperties.ServiceId);
                        insertCommand.Parameters.AddWithValue("@ToServiceName", toService.ServiceName);
                        insertCommand.Parameters.AddWithValue("@ToServiceId", toService.ServiceId);
                        try
                        {
                            await connection.OpenAsync();
                            var sessionId = await insertCommand.ExecuteScalarAsync();
                            if (!Convert.IsDBNull(sessionId))
                            {
                                return Guid.Parse(sessionId.ToString());
                            }
                        }
                        catch(System.Exception exception)
                        {
                            throw new Exception(exception.Message);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> SessionValidate(Guid sessionId, Guid toServiceId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (SqlCommand selectCommand = connection.CreateCommand())
                {
                    selectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    selectCommand.CommandText = "proc_SessionsSelect";
                    selectCommand.Parameters.AddWithValue("@FromServiceId", toServiceId);
                    selectCommand.Parameters.AddWithValue("@ToServiceId", _thisServiceProperties.ServiceId);
                    selectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    try
                    {
                        await connection.OpenAsync();

                        var result = await selectCommand.ExecuteScalarAsync();
                        return Convert.ToBoolean(result);
                    }
                    catch (System.Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }
        }
    }
}
