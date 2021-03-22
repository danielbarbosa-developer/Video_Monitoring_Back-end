using System;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Exceptions;
using Backend.Domain.Entities;

namespace Backend.Application.Handlers
{
    public class RecyclerVideosHandler : IRecycler
    {
        private readonly IRepository<Video> _repository;
        private readonly IService<VideoDtoInput, VideoDto> _service;
        public string Status { get; set; } = "Not Running";
        
        public RecyclerVideosHandler(IService<VideoDtoInput, VideoDto> service, IRepository<Video> repository)
        {
            _service = service;
            _repository = repository;
        }
        public async void RecycleAllVideos(long days)
        {
            try
            {
                Status = "Running";
                var videos = await _repository.GetAllFilter(days);
                foreach (var video in videos)
                {
                    string id = video.VideoId.ToString();
                    await _service.DropAsync(id);
                }

                Status = "Concluded";
            }
            catch (Exception e)
            {
                Status = "Failed and Canceled";
                Console.WriteLine(e);
                throw new ApiException("Falha ao tentar apagar os registros", e);
            }
           
        }
    }
}