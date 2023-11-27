using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebAppTest.Infrastructure;

namespace WebAppTest.DataLayer
{
    public class LogWriter
    {
        private readonly string _connectionString;
        public LogWriter(IOptions<List<DbConnection>> dbConnections)
        {
            _connectionString = dbConnections.Value.FirstOrDefault(item => item.ServerName == "BreweryApp_LogDB").ConnectionString;
        }

        public async Task LogWriteAsync(LogRecord logRecord)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                using (SqlCommand insertCommand = connection.CreateCommand())
                {
                    insertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    insertCommand.CommandText = "proc_LogInsert";
                    insertCommand.Parameters.AddWithValue("@RequestId", Guid.Parse(logRecord.RequestId));
                    insertCommand.Parameters.AddWithValue("@ApplicationName", logRecord.ApplicationName);
                    insertCommand.Parameters.AddWithValue("@Path", logRecord.Path);
                    insertCommand.Parameters.AddWithValue("@Parameters", logRecord.Parameters);
                    insertCommand.Parameters.AddWithValue("@StartAction", logRecord.StartAction);
                    insertCommand.Parameters.AddWithValue("@EndAction", logRecord.EndAction);
                    insertCommand.Parameters.AddWithValue("@ResponseStatusCode", logRecord.ResponseStatusCode);
                    insertCommand.Parameters.AddWithValue("@Result", logRecord.Result);
                    insertCommand.Parameters.AddWithValue("@Exeption", logRecord.Exeption);

                    try
                    {
                        await connection.OpenAsync();
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
        }
    }
}
