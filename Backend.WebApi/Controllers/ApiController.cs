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
        private readonly IRecycler _recycler;
        public ApiController(IRecycler recycler, IService<ServerDtoInput, ServerDto> serverService,IVideoService<VideoInformationDto> specialService, IService<VideoDtoInput, VideoDto> videoService)
        {
            _serverService = serverService;
            _videoService = videoService;
            _specialService = specialService;
            _recycler = recycler;
        }
        [HttpGet ("servers")]
        public async Task<ActionResult<IEnumerable<ServerDto>>> GetServers()
        {
            var list = await _serverService.GetAllAsync();
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
        [HttpGet ("servers/{serverId}/videos/{videoId}")]
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
        [HttpGet("servers/{serverId}/videos/{videoId}/binary")]
        public async Task<ActionResult<string>> DownloadBinaryVideo(string serverId, string videoId)
        {
            var video = await _specialService.DownloadVideo(videoId);
            return Ok(video);
        }

        [HttpPost("recycler/process/{date}")]
        public ActionResult RecycleVideos(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            var timestamp = dateTime.Ticks;
            Task.Factory.StartNew(() => _recycler.RecycleAllVideos(timestamp));
            return Ok();
        }

        [HttpGet("recycle/status")]
        public ActionResult<string> GetRecyclerStatus()
        {
            return Ok(_recycler.Status);
        }
    }
}