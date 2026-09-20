using Microsoft.Data.SqlClient;
using System.Data;

namespace HRM.API.Helpers
{
    public class AdoHelper
    {
        private readonly IConfiguration _configuration;

        public AdoHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }

        public SqlCommand CreateStoredProcedureCommand(SqlConnection connection,string storedProcedureName,params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcedureName,connection);

            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            command.Connection.Open();

            return command.ExecuteReader();
        }
        public int ExecuteNonQuery(SqlCommand command)
        {
            command.Connection.Open();
            return command.ExecuteNonQuery();
        }
        public object? ExecuteScalar(SqlCommand command)
        {
            command.Connection.Open();

            return command.ExecuteScalar();
        }
    }
}