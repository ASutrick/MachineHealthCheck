using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class MachineInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string MachineName { get; set; } = null!;
        public ICollection<HealthCheck> HealthChecks { get; set; } = null!;
    }
}
