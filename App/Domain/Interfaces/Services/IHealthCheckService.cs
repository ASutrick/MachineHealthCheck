using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface IHealthCheckService
    {
        Task<IList<HealthCheck>> GetAll(string key);
        Task<HealthCheck> GetMostRecent(string key);
    }
}
