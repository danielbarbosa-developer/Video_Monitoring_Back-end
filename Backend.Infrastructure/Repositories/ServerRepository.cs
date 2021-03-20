using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Domain.Entities;
using Backend.Infrastructure.Exceptions;
using Dapper;
using MySql.Data.MySqlClient;
using Npgsql;

namespace Backend.Infrastructure.Repositories
{
    public class ServerRepository :  IRepository<Server>
    {
        private readonly IDatabaseConnection _databaseConnection;

        public ServerRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public async Task<Guid> InsertAsync(Server entity)
        {
            entity.GenerateGuid(); // Sets the Guid to be added in database
            await using NpgsqlConnection connection = (NpgsqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "INSERT INTO server VALUES(@server_id, @server_name, @ip_address, @port);";
                var result = await connection.ExecuteScalarAsync(sql,
                    new
                    {
                        server_id = entity.ServerId, 
                        server_name = entity.Name, 
                        ip_address = entity.IpAddress, 
                        port = entity.Port
                    });
                return new Guid();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new RepositoryException("Failed to try to persist in the database", e);
            }
        }

        public async Task InsertAsync(List<Server> entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Server entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(Server entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<Server> GetByIdAsync(object id)
        {
            await using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM server WHERE server_id = @server_id;";
                Server server = await connection.QuerySingleAsync<Server>(sql, 
                    new
                    {
                        server_id = id
                    });
                return server;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new RepositoryException("Failed to try to access the record in the database", e);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Server, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}