using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class DiskInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid HealthCheckId { get; set; }
        public HealthCheck HealthCheck { get; set; } = null!;
        public ulong CapacityMb { get; set; }
        public long FreeSpaceMb { get; set; }
        public double PercentUtilization { get; set; }
    }
}
