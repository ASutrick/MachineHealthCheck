using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Interfaces.Services
{
    public interface IWorkQueueService
    {
        Task QueueWork(string key);
        Task<WorkQueue> DequeueWork();
    }
}
