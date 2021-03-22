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
    public class VideoRepository : IRepository<Video>
    {
        private readonly IDatabaseConnection _databaseConnection;

        public VideoRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public Task<IEnumerable<Video>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Video>> GetAllFilter(string filter)
        {
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM videos WHERE serverid = @server_id;";
                var videos = await connection.QueryAsync<Video>(sql, new
                {
                    server_id = filter
                });
                return videos;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new RepositoryException("Failed to try to access the record in the database", e);
            }
        }

        public async Task<IEnumerable<Video>> GetAllFilter(long filter)
        {
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM videos WHERE timestamp <= @timestamp;";
                var videos = await connection.QueryAsync<Video>(sql, new
                {
                    timestamp = filter
                });
                return videos;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new RepositoryException("Failed to try to access the record in the database", e);
            }
        }

        public async Task<Guid> InsertAsync(Video entity)
        {
            entity.GenerateGuid(); // Sets the Guid to be added in database
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                await connection.OpenAsync();
                string sql =
                    "INSERT INTO videos(videoid, serverid, description, timestamp) VALUES(@video_id, @server_id, @description, @timestamp);";
                var result = await connection.ExecuteScalarAsync(sql,
                    new
                    {
                        video_id = entity.VideoId,
                        server_id = entity.ServerId,
                        description = entity.Description,
                        timestamp = DateTime.Now.Ticks
                    });
                return entity.VideoId;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new RepositoryException("Failed to try to persist in the database", e);
            }
        }

        public async Task InsertAsync(List<Video> entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Video entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(Guid id)
        {
            using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "DELETE FROM videos WHERE videoid = @video_id;";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        video_id = id
                    });
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new RepositoryException("Failed to try to delete the record in the database", e);
            }
        }

        public async Task DropAsync(Video entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<Video> GetByIdAsync(Guid id)
        {
            await using MySqlConnection connection = (MySqlConnection)_databaseConnection.GetConnection();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM videos WHERE videoid = @videoid;";
                Video video = await connection.QuerySingleAsync<Video>(sql, 
                    new
                    {
                        videoid = id
                    });
                return video;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new RepositoryException("Failed to try to access the record in the database", e);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Video, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}