namespace HealthCheck.Host
{
    public class Client
    {
        public string ConnId { get; set; }
        public string? Key { get; set; } 
        public bool HasKey => !String.IsNullOrEmpty(Key);
    }
}
