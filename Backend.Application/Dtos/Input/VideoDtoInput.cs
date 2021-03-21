using System;
using Backend.Abstractions.ApplicationAbstractions;

namespace Backend.Application.Dtos.Input
{
    public class VideoDtoInput : IDto
    {
        public Guid ServerId { get; private set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
        public void AssingServerId(string id)
        {
            ServerId = Guid.Parse(id);
        }
    }
}