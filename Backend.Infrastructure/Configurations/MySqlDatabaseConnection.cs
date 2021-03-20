using System;
using System.Data;
using Backend.Abstractions.InfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Backend.Infrastructure.Configurations
{
    public class MySqlDatabaseConnection : IDatabaseConnection
    {
        public string ConnectionString { get; set; }
        public MySqlDatabaseConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("MySqlConnection");
#if DEBUG
            
            Console.WriteLine($"NpgsqlConnection StringConnection: {this.ConnectionString}");
#endif
        }
        public IDbConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}