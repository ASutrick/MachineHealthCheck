using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class CPUInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid HealthCheckId { get; set; }
        public string Name { get; set; } = "";
        public int NumOfCores { get; set; } = 0;
        public int NumOfLogicalProcessors { get; set; } = 0;
        public int CurrClockSpeed { get; set; } = 0;
        public int PercentInUse { get; set; } = 0;
        public HealthCheck HealthCheck { get; set; } = null!;
    }
}
