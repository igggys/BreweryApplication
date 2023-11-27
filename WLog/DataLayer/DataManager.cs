using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WLog.Infrastructure;
using WLog.Models;

namespace WLog.DataLayer
{
    public class DataManager
    {
        private readonly string _connectionString;
        public DataManager(WLogConfigurationManager wLogConfigurationManager)
        {
            if(wLogConfigurationManager.CanRead)
                _connectionString = wLogConfigurationManager.Settings.ConnectionString;
        }

        public async Task WriteAsync(LogRecord logRecord)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "proc_WLog_Insert";
                    command.Parameters.AddWithValue("@RequestId", logRecord.RequestId);
                    command.Parameters.AddWithValue("@ApplicationName", logRecord.ApplicationName);
                    command.Parameters.AddWithValue("@StartAction", logRecord.Start);
                    command.Parameters.AddWithValue("@Url", logRecord.Url);
                    command.Parameters.AddWithValue("@Method", logRecord.Method);
                    command.Parameters.AddWithValue("@Arguments", logRecord.Arguments);
                    command.Parameters.AddWithValue("@Messages", JsonConvert.SerializeObject(logRecord.Messages));
                    command.Parameters.AddWithValue("@Result", logRecord.Result);
                    command.Parameters.AddWithValue("@Exception", logRecord.Exception);
                    command.Parameters.AddWithValue("@EndAction", logRecord.End);
                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }

        public void Write(LogRecord logRecord)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "proc_WLog_Insert";
                    command.Parameters.AddWithValue("@RequestId", logRecord.RequestId);
                    command.Parameters.AddWithValue("@ApplicationName", logRecord.ApplicationName);
                    command.Parameters.AddWithValue("@StartAction", logRecord.Start);
                    command.Parameters.AddWithValue("@Url", logRecord.Url);
                    command.Parameters.AddWithValue("@Method", logRecord.Method);
                    command.Parameters.AddWithValue("@Arguments", logRecord.Arguments);
                    command.Parameters.AddWithValue("@Messages", JsonConvert.SerializeObject(logRecord.Messages));
                    command.Parameters.AddWithValue("@Result", logRecord.Result);
                    command.Parameters.AddWithValue("@Exception", logRecord.Exception);
                    command.Parameters.AddWithValue("@EndAction", logRecord.End);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }
    }
}
