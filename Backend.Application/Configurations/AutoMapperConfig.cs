using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;

namespace Backend.Application.Configurations
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration = null!; 
        
        public static void InicializeAutoMapper()
        {
            Configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<Server, ServerDto>();
                config.CreateMap<ServerDto, Server>();
                config.CreateMap<ServerDtoInput, Server>();
                config.CreateMap<Video, VideoDto>();
                config.CreateMap<VideoDto, Video>();
                config.CreateMap<VideoDtoInput, Video>();
                config.CreateMap<VideoInformationDto, Video>();
                config.CreateMap<Video, VideoInformationDto>();
            });
        }
        public static T Map<T>(object source) => Configuration.CreateMapper().Map<T>(source);
    }
}