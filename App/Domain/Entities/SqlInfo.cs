using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineHealthCheck.Domain.Entities
{
    public class SqlInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid HealthCheckId { get; set; }
        public HealthCheck HealthCheck { get; set; } = null!;
        public bool HasSqlServer { get; set; } = false;
        public string SqlServerVersion { get; set; }
    }
}
