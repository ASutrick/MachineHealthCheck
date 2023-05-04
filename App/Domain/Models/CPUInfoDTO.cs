using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class CPUInfoDTO
    {
        public string Name { get; set; } = "";
        public int NumOfCores { get; set; } = 0;
        public int NumOfLogicalProcessors { get; set; } = 0;
        public int CurrClockSpeed { get; set; } = 0;
        public int PercentInUse { get; set; } = 0;
        public static CPUInfoDTO FromEntity(CPUInfo i)
        {
            CPUInfoDTO d = new CPUInfoDTO();
            d.Name = i.Name;
            d.NumOfCores = i.NumOfCores;
            d.NumOfLogicalProcessors = i.NumOfLogicalProcessors;
            d.CurrClockSpeed = i.CurrClockSpeed;
            d.PercentInUse = i.PercentInUse;
            return d;
        }
        public CPUInfo ToEntity()
        {
            CPUInfo m = new CPUInfo();
            m.Name = Name;
            m.NumOfCores = NumOfCores;
            m.NumOfLogicalProcessors = NumOfLogicalProcessors;
            m.CurrClockSpeed = CurrClockSpeed;
            m.PercentInUse = PercentInUse;
            return m;
        }
    }
}
