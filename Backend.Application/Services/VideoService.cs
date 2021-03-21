using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Exceptions;
using Backend.Application.Validators;
using Backend.Domain.Entities;
using FluentValidation;

namespace Backend.Application.Services
{
    public class VideoService : IService<VideoDtoInput, VideoDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Video> _repository;
        private readonly AbstractValidator<VideoDtoInput> _validator;
        public VideoService(IMapper mapper, IRepository<Video> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = new VideoInputValidator();
        }
        public async Task<string> InsertAsync(VideoDtoInput entity)
        {
            try
            {
                var validate = await _validator.ValidateAsync(entity);
                if (!validate.IsValid)
                {
                    return validate.ToString();
                }
                var guid = await _repository.InsertAsync(_mapper.Map<Video>(entity));
                return guid.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                return new ApiException("Erro ao fazer upload do video", e).Message;
            }
        }

        public async Task InsertAsync(List<VideoDtoInput> entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(VideoDtoInput entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(string id)
        {
            var guid = Guid.Parse(id);
            await _repository.DropAsync(guid);
        }

        public async Task DropAsync(VideoDtoInput entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<VideoDto> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}