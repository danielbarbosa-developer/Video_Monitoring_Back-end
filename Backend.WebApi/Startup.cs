using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Abstractions.InfrastructureAbstractions;
using Backend.Application.Configurations;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Handlers;
using Backend.Application.Services;
using Backend.Domain.Entities;
using Backend.Infrastructure.Configurations;
using Backend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Backend.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding abstractions and their implementations to the services container
            services.AddTransient<IService<ServerDtoInput, ServerDto>, ServerService>();
            services.AddTransient<IService<VideoDtoInput, VideoDto>, VideoService>();
            services.AddTransient<IVideoService<VideoInformationDto>, VideoService>();
            services.AddTransient<IDatabaseConnection, MySqlDatabaseConnection>();
            services.AddTransient<IRepository<Server>, ServerRepository>();
            services.AddTransient<IRepository<Video>, VideoRepository>();
            services.AddSingleton<IRecycler, RecyclerVideosHandler>();
            //____________________________________________________________________________________
            // Automapper configuration
            AutoMapperConfig.InicializeAutoMapper();
            services.AddSingleton<IMapper>(provider => new Mapper(AutoMapperConfig.Configuration));
            //_____________________________________________________________________________________
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Backend.WebApi", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(env.WebRootPath, "App_Data"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}