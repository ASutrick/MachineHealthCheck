using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace HealthCheck.Host
{
    public class VerificationService : IVerificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VerificationService> _logger;
        public VerificationService(IUnitOfWork unitOfWork, ILogger<VerificationService> logger)
        {
            _unitOfWork=unitOfWork;
            _logger=logger;
        }
        public async Task<MachineInfo> Verify(string key)
        {
            IQueryable<MachineInfo> machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
            _logger.LogInformation(key, machines.Count());
            var m = machines.First();

            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.isVerified = true;

                await _unitOfWork.CommitTransaction();
                m = work;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
            return m;
        }
    }
}
