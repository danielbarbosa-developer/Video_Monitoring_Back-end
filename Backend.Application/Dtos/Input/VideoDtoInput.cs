using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos.Input
{
    public class VideoDtoInput : IDto
    {
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
    }
}