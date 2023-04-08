using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace HealthCheck.Host
{
    public class HealthCheckHubService : IHealthCheckHubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HealthCheckHubService> _logger;
        public HealthCheckHubService(IUnitOfWork unitOfWork, ILogger<HealthCheckHubService> logger)
        {
            _unitOfWork=unitOfWork;
            _logger=logger;
        }
        public async Task<bool> Verify(string key)
        {
            
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
            MachineInfo m;
            try
            {
                m = machines.First();
            }
            catch(InvalidOperationException e) { return false; }

            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.isVerified = true;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
            return true;
        }
    }
}
