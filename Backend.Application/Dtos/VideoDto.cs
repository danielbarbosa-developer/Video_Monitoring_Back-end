using System;
using System.Buffers.Text;
using System.Security.Cryptography;
using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos
{
    public class VideoDto : IDto
    {
        public Guid VideoId { get; set; }
        public Guid ServerId { get; set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
    }
}