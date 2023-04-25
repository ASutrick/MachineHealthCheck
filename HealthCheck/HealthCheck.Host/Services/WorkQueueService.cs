using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;

namespace HealthCheck.Host.Services
{
    public class WorkQueueService : IWorkQueueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkQueueService> _logger;
        public WorkQueueService(IUnitOfWork unitOfWork, ILogger<WorkQueueService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<WorkQueue?> DequeueWork()
        {
            WorkQueue one;
            IQueryable<WorkQueue> actives = await _unitOfWork.Repository<WorkQueue>().FindByCondition(q => q.isActive);
            try 
            {
                if (actives.Any())
                {
                    one = actives.OrderBy(a => a.Id).First();
                }
                else
                {
                    _logger.LogInformation("No queued work found.");
                    return null;
                }
            }
            catch (Exception ex) { _logger.LogError(ex,"There was issue dequeueing work!"); return null; }

            try
            {
                await _unitOfWork.BeginTransaction();
                var workRepos = _unitOfWork.Repository<WorkQueue>();
                var work = await workRepos.FindAsync(one.Id);
                if (work == null)
                    throw new KeyNotFoundException();
                work.isActive = false;
                await _unitOfWork.CommitTransaction();
                one = work;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
            return one;
        }

        public async Task QueueWork(string key)
        {
            throw new NotImplementedException();
        }
    }
}
