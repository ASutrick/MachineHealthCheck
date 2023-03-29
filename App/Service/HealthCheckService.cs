using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;

namespace MachineHealthCheck.Service
{
    internal class HealthCheckService : IHealthCheckService
    {
        public Task Add(HealthCheck machine)
        {
            throw new NotImplementedException();
        }

        public Task Delete(HealthCheck machine)
        {
            throw new NotImplementedException();
        }

        public Task<IList<HealthCheck>> GetAll(Guid machineId)
        {
            throw new NotImplementedException();
        }

        public Task<HealthCheck> GetMostRecent(Guid machineId)
        {
            throw new NotImplementedException();
        }

        public Task Update(HealthCheck machine)
        {
            throw new NotImplementedException();
        }
    }
}
