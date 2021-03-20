using System;
using System.Data;
using Backend.Abstractions.InfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Backend.Infrastructure.Configurations
{
    public class PostgresDatabaseConnection : IDatabaseConnection
    {
         public string ConnectionString { get; set; }
        public PostgresDatabaseConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PostgresConnection");
        }
        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}