using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Abstractions.ApplicationAbstractions
{
    public interface IService<TInput, TModel> where TInput : IDto where TModel : IDto
    {
        Task<string> InsertAsync(TInput entity);
        Task InsertAsync(List<TInput> entity);
        Task UpdateAsync(TInput entityToUpdate);
        Task DropAsync(string id);
        Task DropAsync(TInput entityToDelete);
        Task<TModel> GetByIdAsync(string id);
        Task<IEnumerable<TModel>> GetAllAsync();
    }
}