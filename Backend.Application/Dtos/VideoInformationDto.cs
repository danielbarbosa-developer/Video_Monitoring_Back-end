using System;
using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos
{
    public class VideoInformationDto : IDto
    {
        public Guid VideoId { get; set; }
        public Guid ServerId { get; set; }
        public string Description { get; set; } = null!;
    }
}