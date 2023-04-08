using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Interfaces.Services
{
    public interface IWorkQueueService
    {
        Task QueueWork(string key);
        Task<WorkQueue> DequeueWork();
    }
}
