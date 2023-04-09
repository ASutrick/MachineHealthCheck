using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface IHealthCheckService
    {
        Task<IList<HealthCheck>> GetAll(string key);
        Task<HealthCheck> GetMostRecent(string key);
    }
}
