using System;

namespace Backend.Application.Dtos
{
    public class ServerDto
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }
    }
}