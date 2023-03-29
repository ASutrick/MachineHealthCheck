using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class HealthCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid MachineInfoId { get; set; }
        public DateTime Date { get; set; }
        public string OperatingSystem { get; set; }
        public string OSVersion { get; set; }
        public MachineInfo MachineInfo { get; set; } = null!;
        public ICollection<CPUInfo> CPUInfo { get; set; } = null!;
        public MemoryInfo MemoryInfo { get; set; } = null!;
        public SqlInfo SqlInfo { get; set; } = null!;
        public ICollection<DiskInfo> DiskInfo { get; set; } = null!;
    }
}
