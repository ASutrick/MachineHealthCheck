using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MachineHealthCheck.Service
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HealthCheckService> _logger;
        public HealthCheckService(IUnitOfWork unitOfWork, ILogger<HealthCheckService> logger) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<HealthCheck>?> GetAll(string key)
        {
            MachineInfo m;
            List<HealthCheck>? checks = null;
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key && m.isActive);
           
            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError(e.Message);
                return null; 
            }
            IQueryable<HealthCheck>? healthChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
            try
            {
                checks = healthChecks.OrderByDescending(check => check.Date).ToList();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError(e.Message);
                return null; 
            }
            return checks;
        }

        public async Task<HealthCheck?> GetMostRecent(string key)
        {
            MachineInfo m;
            HealthCheck? check = null;
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key && m.isActive);

            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError(e.Message);
                return null; 
            }
            IQueryable<HealthCheck>? healthChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
            try
            {
                check = healthChecks.OrderByDescending(check => check.Date).FirstOrDefault();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError($"No health checks found for key: {key}", e);
                return null; 
            }
            return check;
        }

        public async Task<HealthCheck?> WaitForNext(string key)
        {
            MachineInfo m;
            HealthCheck? check;
            bool finished = false;
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key && m.isActive);

            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError(e.Message);
                return null; 
            }
            IQueryable<HealthCheck>? healthChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
            try
            {
                check = healthChecks.OrderByDescending(check => check.Date).FirstOrDefault();
            }
            catch (InvalidOperationException e) 
            {
                _logger.LogError(e.Message);
                return null; 
            }
            if(check == null)
            {
                while(!finished)
                {
                    IQueryable<HealthCheck>? newChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
                    try
                    {
                        check = newChecks.OrderByDescending(check => check.Date).FirstOrDefault();
                    }
                    catch (InvalidOperationException e) 
                    {
                        _logger.LogError(e.Message);
                        return null;
                    }
                    if(check != null)
                    {
                        finished = true;
                    }
                    Thread.Sleep(3000);
                }
                return check;
            }
            else
            {
                var date = check.Date;
                while(!finished)
                {
                    IQueryable<HealthCheck>? newChecks = await _unitOfWork.Repository<HealthCheck>().FindByCondition(check => check.MachineInfo.Id == m.Id, check => check.CPUInfo, check => check.DiskInfo, check => check.SqlInfo, check => check.MemoryInfo);
                    try
                    {
                        check = newChecks.OrderByDescending(check => check.Date).FirstOrDefault();
                    }
                    catch (InvalidOperationException e) 
                    {
                        _logger.LogError(e.Message);
                        return null; 
                    }
                    if (check != null)
                    {
                        if (check.Date > date)
                        {
                            finished = true;
                        }
                    }
                    Thread.Sleep(3000);
                }
                return check;
            }
        }
    }
}
