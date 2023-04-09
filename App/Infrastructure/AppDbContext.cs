using Microsoft.EntityFrameworkCore;
using MachineHealthCheck.Domain.Entities;
namespace MachineHealthCheck.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<MachineInfo> MachineInfo { get; set; }
        public DbSet<HealthCheck> HealthCheck { get; set; }
        public DbSet<CPUInfo> CPUInfo { get; set; }
        public DbSet<DiskInfo> DiskInfo { get; set; }
        public DbSet<MemoryInfo> MemoryInfo { get; set; }
        public DbSet<SqlInfo> SqlInfo { get; set; }
        public DbSet<WorkQueue> WorkQueue { get; set; }
    }
}
