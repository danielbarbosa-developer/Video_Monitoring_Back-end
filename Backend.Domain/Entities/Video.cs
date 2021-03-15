using System;

namespace Backend.Domain.Entities
{
    public class Video
    {
        public Guid VideoId { get; set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
    }
}