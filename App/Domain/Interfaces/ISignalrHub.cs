using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface ISignalrHub
    {
        Task HealthCheckRequest(string info, string key);
        Task HealthCheckResponse(MachineInfo info, string key);
        Task VerifyKey(string key);

    }
}
