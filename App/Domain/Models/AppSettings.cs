namespace MachineHealthCheck.Domain.Models
{
    public class AppSettings
    {
        public static string ConnectionString { get; private set; } = null!;
        public static string[] CORS { get; private set; } = null!;
    }
}
