using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;

namespace Backend.Application.Services
{
    public class VideoService : IService<VideoDtoInput, VideoDto>
    {
        public async Task<string> InsertAsync(VideoDtoInput entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(List<VideoDtoInput> entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(VideoDtoInput entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(VideoDtoInput entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<VideoDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}