using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.Dtos;

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
            });
        }
        public static T Map<T>(object source) => Configuration.CreateMapper().Map<T>(source);
    }
}