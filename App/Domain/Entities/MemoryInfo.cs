using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class MemoryInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid HealthCheckId { get; set; }
        public HealthCheck HealthCheck { get; set; } = null!;
        public ulong TotalPhysicalMb { get; set; }
        public double PercentInUse { get; set; }
    }
}
