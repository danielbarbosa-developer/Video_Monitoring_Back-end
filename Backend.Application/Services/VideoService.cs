using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Exceptions;
using Backend.Application.Handlers;
using Backend.Application.Validators;
using Backend.Domain.Entities;
using FluentValidation;

namespace Backend.Application.Services
{
    public class VideoService : IService<VideoDtoInput, VideoDto>, IVideoService<VideoInformationDto>
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
                string identifier = guid.ToString();
                
                string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                var fileName = $"{path}\\{identifier}.mp4";
                VideoFileHandler.SaveVideoAsBinaryFile(entity.VideoContent,fileName);
                return identifier;
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
            try
            {
                var guid = Guid.Parse(id);
                await _repository.DropAsync(guid);
                string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                var fileName = $"{path}\\{guid}.mp4";
                File.Delete(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApiException("Falha ao deletar video", e);
            }
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

        public Task<string> DownloadVideo(string id)
        {
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var fileName = $"{path}\\{id}.mp4";
            var base64String = VideoFileHandler.ConvertVideoToBase64(fileName);
            return Task.FromResult(base64String);
        }

        public async Task<VideoInformationDto> GetVideoInformation(string id)
        {
            var guid = Guid.Parse(id);
            return _mapper.Map<VideoInformationDto>(await _repository.GetByIdAsync(guid));
        }
        public async Task<IEnumerable<VideoInformationDto>> GetAllVideosInformation(string serverId)
        {
            var guid = Guid.Parse(serverId);
            var response = await _repository.GetAllFilter(serverId);
            List<VideoInformationDto> videosDtos = new List<VideoInformationDto>();
            foreach (var video in response)
            {
                videosDtos.Add(_mapper.Map<VideoInformationDto>(video));
            }
            return videosDtos;
        }
    }
}