using System.Data;
using Backend.Abstractions.InfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Backend.Infrastructure.Configurations
{
    public class MySqlDatabaseConnection : IDatabaseConnection
    {
        public string ConnectionString { get; set; }
        public MySqlDatabaseConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("MySqlConnection");
        }
        public IDbConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}