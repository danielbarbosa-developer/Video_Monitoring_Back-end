﻿using System;
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
    public class ServerService : IService<ServerDtoInput, ServerDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Server> _repository;
        private readonly AbstractValidator<ServerDtoInput> _validator;
        public ServerService(IMapper mapper, IRepository<Server> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = new ServerInputValidator();
        }
        public async Task<string> InsertAsync(ServerDtoInput entity)
        {
            try
            {
                var validate = await _validator.ValidateAsync(entity);
                if (!validate.IsValid)
                {
                    return validate.ToString();
                }
                var guid = await _repository.InsertAsync(_mapper.Map<Server>(entity));
                return guid.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                return new ApiException("Erro ao tentar inserir o registro", e).Message;
            }
        }

        public async Task InsertAsync(List<ServerDtoInput> entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ServerDtoInput entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DropAsync(ServerDtoInput entityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<ServerDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}