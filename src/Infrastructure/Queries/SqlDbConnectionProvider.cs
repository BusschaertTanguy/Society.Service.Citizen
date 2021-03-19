using System.Data;
using Application.Connections;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Queries
{
    public class SqlDbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public SqlDbConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}