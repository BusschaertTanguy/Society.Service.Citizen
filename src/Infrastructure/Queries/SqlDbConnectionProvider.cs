using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Queries
{
    internal class SqlDbConnectionProvider
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