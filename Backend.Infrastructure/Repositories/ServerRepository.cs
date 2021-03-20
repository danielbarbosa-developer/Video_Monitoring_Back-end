using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Domain.Entities;
using Backend.Infrastructure.Exceptions;
using Dapper;
using MySqlConnector;


namespace Backend.Infrastructure.Repositories
{
    public class ServerRepository :  IRepository<Server>
    {
        private readonly IDatabaseConnection _databaseConnection;

        public ServerRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public async Task<IEnumerable<Server>> GetAll()
        {
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM servers;";
                var servers = await connection.QueryAsync<Server>(sql);
                return servers;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new RepositoryException("Failed to try to access the record in the database", e);
            }
        }

        public async Task<Guid> InsertAsync(Server entity)
        {
            entity.GenerateGuid(); // Sets the Guid to be added in database
            using (var connection =
                new MySqlConnection("Server=127.0.0.1;Port=3306;Database=Video_Monitoring;Uid=root;Pwd=RootPassword;"))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql =
                        "INSERT INTO servers(serverid, name, ipaddress, port) VALUES(@server_id, @name, @ip_address, @port);";
                    var result = await connection.ExecuteScalarAsync(sql,
                        new
                        {
                            server_id = entity.ServerId,
                            name = entity.Name,
                            ip_address = entity.IpAddress,
                            port = entity.Port
                        });
                    return entity.ServerId;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message, e.StackTrace);
                    Console.WriteLine(e);
                    throw new RepositoryException("Failed to try to persist in the database", e);
                }
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
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "DELETE FROM servers WHERE serverid = @server_id;";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        server_id = id
                    });
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new RepositoryException("Failed to try to delete the record in the database", e);
            }
        }

        public async Task DropAsync(Server entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<Server> GetByIdAsync(Guid id)
        {
            await using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM servers WHERE serverid = @serverid;";
                Server server = await connection.QuerySingleAsync<Server>(sql, 
                    new
                    {
                        serverid = id
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