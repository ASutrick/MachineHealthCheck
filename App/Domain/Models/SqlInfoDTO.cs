using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Models
{
    public class SqlInfoDTO
    {
        public bool HasSqlServer { get; set; } = false;
        public string SqlServerVersion { get; set; }
        public static SqlInfoDTO FromEntity(SqlInfo i)
        {
            SqlInfoDTO dto = new SqlInfoDTO();
            dto.HasSqlServer = i.HasSqlServer;
            dto.SqlServerVersion = i.SqlServerVersion;

            return dto;
        }
        public SqlInfo ToEntity()
        {
            SqlInfo info = new SqlInfo();
            info.HasSqlServer = HasSqlServer;
            info.SqlServerVersion = SqlServerVersion;
            return info;
        }
    }
}
