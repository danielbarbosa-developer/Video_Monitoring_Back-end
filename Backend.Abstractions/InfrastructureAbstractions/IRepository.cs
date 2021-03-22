using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Abstractions.DomainAbstractions;

namespace Backend.Abstractions.InfrastructureAbstractions
{
    public interface IRepository<TModel> where TModel : IEntity
    {
        Task<Guid> InsertAsync(TModel entity);
        Task InsertAsync(List<TModel> entity);
        Task UpdateAsync(TModel entityToUpdate);
        Task DropAsync(Guid id);
        Task DropAsync(TModel entityToDelete);
        Task<TModel> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> GetAll();
        Task<IEnumerable<TModel>> GetAllFilter(string filter);
        Task<IEnumerable<TModel>> GetAllFilter(long filter);
        Task<bool> ExistsAsync(Expression<Func<TModel, bool>> filter);
    }
}