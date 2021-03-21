using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IService<ServerDtoInput, ServerDto> _serverService;
        private readonly IService<VideoDtoInput, VideoDto> _videoService;
        private readonly IVideoService<VideoInformationDto> _specialService;
        public ApiController(IService<ServerDtoInput, ServerDto> serverService,IVideoService<VideoInformationDto> specialService, IService<VideoDtoInput, VideoDto> videoService)
        {
            _serverService = serverService;
            _videoService = videoService;
            _specialService = specialService;
        }
        [HttpGet ("Servers")]
        public async Task<ActionResult<IEnumerable<ServerDto>>> GetServers()
        {
            var list = await _serverService.GetAllAsync();
            /*
            List<Server> list = new List<Server>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Server
                {
                    ServerId = Guid.NewGuid(),
                    Name = "LocalServer",
                    IpAddress = "127.0.1.0",
                    Port = 8080
                });
            }
            */
            return Ok(list);
        }
        [HttpGet("servers/{serverId}")]
        public async Task<ActionResult<ServerDto>> GetServer(string? serverId)
        {
            if (serverId == null)
            {
                return BadRequest();
            }
            var server = await _serverService.GetByIdAsync(serverId);
            return Ok(server);
        }
        /// <summary>
        /// Used to create a new server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpPost ("server")]
        public async Task<ActionResult<string>> PostServer([FromBody] ServerDtoInput server)
        {
            string response = await _serverService.InsertAsync(server);
            return Ok(response); 
        }

        [HttpPost("servers/{serverId}/videos")]
        public async Task<ActionResult<string>> PostVideo(string serverId, [FromBody] VideoDtoInput video)
        {
            video.AssingServerId(serverId);
            string response = await _videoService.InsertAsync(video);
            return Ok(response);
        }
        [HttpDelete ("server/{serverId}")]
        public async Task<ActionResult<string>> DeleteServer(string serverId)
        {
            if (String.IsNullOrEmpty(serverId))
            {
                return BadRequest();
            }
            await _serverService.DropAsync(serverId);
            return Ok();

        }
        [HttpGet ("server/{serverId}/videos/{videoId}")]
        public async Task<ActionResult<VideoInformationDto>> GetVideo(string serverId, string videoId)
        {
            var response = await _specialService.GetVideoInformation(videoId);
            return Ok(response);
        }

        [HttpGet("servers/{serverId}/videos")]
        public async Task<ActionResult<IEnumerable<VideoInformationDto>>> GetAllVideos(string serverId)
        {
            var response = await _specialService.GetAllVideosInformation(serverId);
            return Ok(response);
        }

        [HttpDelete("servers/{serverId}/videos/{videoId}")]
        public async Task<ActionResult> DeleteVideo(string serverId, string videoId)
        {
            await _videoService.DropAsync(videoId);
            return Ok();
        }
    }
}