using System;
using Backend.Abstractions.DomainAbstractions;

namespace Backend.Domain.Entities
{
    public class Video : IEntity
    {
        public Guid VideoId { get; set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
    }
}