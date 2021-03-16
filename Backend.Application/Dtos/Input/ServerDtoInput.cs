using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos.Input
{
    public class ServerDtoInput : IDto
    {
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }
    }
}