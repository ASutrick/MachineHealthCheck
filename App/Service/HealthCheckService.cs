using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;

namespace MachineHealthCheck.Service
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HealthCheckService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<HealthCheck>?> GetAll(string key)
        {
            MachineInfo m;
            List<HealthCheck> checks = null;
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
           
            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) { return null; }
            IQueryable<HealthCheck>? healthChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
            try
            {
                checks = healthChecks.OrderByDescending(check => check.Date).ToList();
            }
            catch (InvalidOperationException e) { return null; }
            return checks;
        }

        public async Task<HealthCheck> GetMostRecent(string key)
        {
            MachineInfo m;
            HealthCheck check = null;
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);

            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) { return null; }
            IQueryable<HealthCheck>? healthChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
            try
            {
                check = healthChecks.OrderByDescending(check => check.Date).FirstOrDefault();
            }
            catch (InvalidOperationException e) { return null; }
            return check;
        }
    }
}
