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
        public ActionResult<IEnumerable<Server>> GetServers()
        {
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

            return Ok(list);
        }
        /// <summary>
        /// Used to create a new server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [HttpPost ("server")]
        public async Task<ActionResult<string>> PostServer([FromBody] ServerDtoInput server)
        {
            if (server == null)
            {
                return BadRequest();
            }
            string response = await _serverService.InsertAsync(server);
            return Ok(response); 
        }
        [HttpDelete ("server/{serverId}")]
        public ActionResult<Server> DeleteServer(string serverId)
        {
            if (serverId != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet ("server/{serverId}/videos/{videoId}")]
        public ActionResult<Server> GetVideo(string serverId, string videoId)
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
    public class Server
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }
    }
}