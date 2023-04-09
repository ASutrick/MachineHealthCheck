using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Interfaces.Services
{
    public interface IHealthCheckHubService
    {
        Task<bool> Verify(string key, string connId);
        Task<bool> Disconnect(string connId);
        Task<bool> AddHealthCheck(HealthCheck healthCheck, string key);
    }
}
