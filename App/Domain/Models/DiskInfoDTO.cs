using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class DiskInfoDTO
    {
        public ulong CapacityMb { get; set; }
        public long FreeSpaceMb { get; set; }
        public double PercentUtilization { get; set; }
        public static DiskInfoDTO FromEntity(DiskInfo i)
        {
            DiskInfoDTO d = new DiskInfoDTO();
            d.CapacityMb = i.CapacityMb;
            d.FreeSpaceMb = i.FreeSpaceMb;
            d.PercentUtilization = i.PercentUtilization;

            return d;
        }
        public DiskInfo ToEntity()
        {
            DiskInfo m = new DiskInfo();
            m.CapacityMb = CapacityMb;
            m.FreeSpaceMb = FreeSpaceMb;
            m.PercentUtilization = PercentUtilization;
            return m;
        }
    }
}
