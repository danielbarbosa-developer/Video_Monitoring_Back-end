using System;
using System.Data;
using Backend.Abstractions.InfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Backend.Infrastructure.Configurations
{
    public class PostgresDatabaseConnection : IDatabaseConnection
    {
        public string StringConnection { get; set; }
        public PostgresDatabaseConnection(IConfiguration configuration)
        {
            this.StringConnection = configuration.GetConnectionString("PostgresConnection");
#if DEBUG
            
            Console.WriteLine($"NpgsqlConnection StringConnection: {this.StringConnection}");
#endif
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(this.StringConnection);
        }
    }
}