using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Abstractions.ApplicationAbstractions;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IService<ServerDtoInput, ServerDto> _serverService;
        public ApiController(IService<ServerDtoInput, ServerDto> serverService)
        {
            _serverService = serverService;
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
        [HttpDelete ("server/{serverId}")]
        public async Task<ActionResult<string>> DeleteServer(string? serverId)
        {
            if (serverId == null)
            {
                return BadRequest();
            }
            await _serverService.DropAsync(serverId);
            return Ok();

        }
        [HttpGet ("server/{serverId}/videos/{videoId}")]
        public ActionResult<VideoDto> GetVideo(string serverId, string videoId)
        {
            if (serverId != null && videoId != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /*
        [HttpGet("{serverId}")]
        public ActionResult GetServer(string serverId)
        {
            
        }
        */
    }
    /*
    public class Server
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }
    }
    */
}