using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class ServerRepository :  IRepository<Server>
    {
        public async Task<Guid> InsertAsync(Server entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Server, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}