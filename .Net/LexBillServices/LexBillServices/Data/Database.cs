using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSql");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

