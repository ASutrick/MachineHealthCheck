using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class MachineInfoDTO
    {

        public string Name { get; set; } = null!;
        public string? Machine { get; set; }
        public string Key { get; set; } = null!;
        public DateTime? LastChecked { get; set; }
        public bool IsVerified { get; set; }
        
        public static MachineInfoDTO FromMI(MachineInfo i)
        {
            MachineInfoDTO d = new MachineInfoDTO();
            d.Name = i.ClientName;
            d.Machine = i.MachineName;
            d.IsVerified = i.isVerified;
            d.Key = i.Key;
            d.LastChecked = i.LastChecked;
            return d;
        }
        public MachineInfo ToMI()
        {
            MachineInfo m = new MachineInfo();
            m.HealthChecks = new List<HealthCheck>();
            m.ClientName = Name;
            m.isVerified = IsVerified;
            m.LastChecked = LastChecked;
            m.MachineName = Machine;
            m.Key = Key;
            m.isVerified = false;
            return m;   
        }
    }
}
