using System;
using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos
{
    public class VideoDto : IDto
    {
        public Guid VideoId { get; set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
    }
}