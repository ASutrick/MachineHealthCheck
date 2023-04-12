using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using MachineHealthCheck.Infrastructure.Migrations;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace HealthCheck.Host.Services
{
    public class HealthCheckHubService : IHealthCheckHubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HealthCheckHubService> _logger;
        public HealthCheckHubService(IUnitOfWork unitOfWork, ILogger<HealthCheckHubService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Disconnect(string connId)
        {
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.ConnectionId == connId);
            MachineInfo m;
            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) { return false; }

            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.isVerified = false;
                work.ConnectionId = null;
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
            return true;
        }
        public async Task<bool> AddHealthCheck(MachineHealthCheck.Domain.Entities.HealthCheck healthCheck, string key)
        {
            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
            MachineInfo m;
            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) { return false; }
            healthCheck.MachineInfoId = m.Id;
            try
            {
                await _unitOfWork.Repository<MachineHealthCheck.Domain.Entities.HealthCheck>().InsertAsync(healthCheck, true);
            }
            catch(Exception e) { return false; }

            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.LastChecked = healthCheck.Date;
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                return false;
            }

            return true;
        }

        public async Task<bool> Verify(string key, string connId)
        {

            IQueryable<MachineInfo>? machines = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
            MachineInfo m;
            try
            {
                m = machines.First();
            }
            catch (InvalidOperationException e) { return false; }

            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.isVerified = true;
                work.ConnectionId = connId;
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
