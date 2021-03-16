using System;
using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos
{
    public class ServerDto : IDto
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }
    }
}