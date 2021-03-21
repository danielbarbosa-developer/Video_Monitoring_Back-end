using System;
using Backend.Abstractions.DomainAbstractions;

namespace Backend.Domain.Entities
{
    public class Video : IEntity
    {
        /// <summary>
        /// Unique Identifier (Primary Key)
        /// </summary>
        public Guid VideoId { get; set; }
        /// <summary>
        /// Foreign key to identify which server this video belongs to
        /// </summary>
        public Guid ServerId { get; set; }
        public string Description { get; set; } = null!;
        public string VideoContent { get; set; } = null!;
        public void GenerateGuid()
        {
            VideoId = Guid.NewGuid();
        }
    }
}