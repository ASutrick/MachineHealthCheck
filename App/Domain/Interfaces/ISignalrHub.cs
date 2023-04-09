using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface ISignalrHub
    {
        Task HealthCheckRequest(string date);
        Task HealthCheckResponse(MachineInfo info, string key);
        Task VerifyKey(string key);

    }
}
