using System.Data;

namespace Backend.Abstractions.InfrastructureAbstractions
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}