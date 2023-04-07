using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class MachineInfoDTO
    {

        public string Name { get; set; } = null!;
        public string? Machine { get; set; }
        public string Key { get; set; } = null!;
        
        public static MachineInfoDTO FromMI(MachineInfo i)
        {
            MachineInfoDTO d = new MachineInfoDTO();
            d.Name = i.ClientName;
            d.Machine = i.MachineName;
            d.Key = i.Key;
            return d;
        }
        public MachineInfo ToMI()
        {
            MachineInfo m = new MachineInfo();
            m.HealthChecks = new List<HealthCheck>();
            m.ClientName = Name;
            m.MachineName = Machine;
            m.Key = Key;
            m.isVerified = false;
            return m;   
        }
    }
}
