using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class HealthCheckDTO
    {
        public DateTime Date { get; set; }
        public ICollection<CPUInfoDTO> CPUInfo { get; set; } = null!;
        public MemoryInfoDTO MemoryInfo { get; set; } = null!;
        public SqlInfoDTO SqlInfo { get; set; } = null!;
        public ICollection<DiskInfoDTO> DiskInfo { get; set; } = null!;

        public static HealthCheckDTO FromEntity(HealthCheck check)
        {
            HealthCheckDTO dto = new HealthCheckDTO();
            dto.Date = check.Date;
            dto.MemoryInfo = MemoryInfoDTO.FromEntity(check.MemoryInfo);
            dto.SqlInfo = SqlInfoDTO.FromEntity(check.SqlInfo);

            ICollection<CPUInfoDTO> cpus = new List<CPUInfoDTO>();
            foreach(var cpu in check.CPUInfo) 
            {
                cpus.Add(CPUInfoDTO.FromEntity(cpu));
            }
            dto.CPUInfo = cpus;

            ICollection<DiskInfoDTO> disks = new List<DiskInfoDTO>();
            foreach(var disk in check.DiskInfo)
            {
                disks.Add(DiskInfoDTO.FromEntity(disk));
            }
            dto.DiskInfo = disks;
            return dto;
        }
    }
}
