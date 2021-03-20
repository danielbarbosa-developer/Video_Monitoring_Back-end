using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class VideoRepository : IRepository<Video>
    {
        public Task<IEnumerable<Video>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> InsertAsync(Video entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task DropAsync(Video entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<Video> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Video, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}