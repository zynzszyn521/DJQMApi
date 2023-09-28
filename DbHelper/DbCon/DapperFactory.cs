using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DbHelper.DbCon
{
    public class DapperFactory
    {
        private readonly string _connectionString;

        public DapperFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionStringName");
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}