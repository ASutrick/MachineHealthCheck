using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Infrastructure.Repositories
{
    public static class MachineInfoRepository
    {
        public static async Task<IList<MachineInfo>> GetAll(this IRepository<MachineInfo> repository)
        {
            var machines = await repository.Entities.OrderBy(e => e.MachineName).ToListAsync();
            return machines;
           
        }
    }
}
