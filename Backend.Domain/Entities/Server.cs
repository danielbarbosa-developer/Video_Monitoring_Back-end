using System;
using Backend.Abstractions.DomainAbstractions;

namespace Backend.Domain.Entities
{
    public class Server : IEntity
    {
        public Server()
        {
            
        }
        public Guid ServerId { get; set; }
        public string Name { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int Port { get; set; }

        public void GenerateGuid()
        {
            ServerId = Guid.NewGuid();
        }
    }
}