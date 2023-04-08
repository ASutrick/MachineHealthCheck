using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Service
{
    public class WorkQueueService : IWorkQueueService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkQueueService(IUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<WorkQueue> DequeueWork()
        {
            IQueryable<WorkQueue> actives = await _unitOfWork.Repository<WorkQueue>().FindByCondition(q => q.isActive);
            WorkQueue one = actives.OrderBy(a => a.Id).First();
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

        public async Task QueueWork(WorkQueue work)
        {
           await _unitOfWork.Repository<WorkQueue>().InsertAsync(work, true);
        }
    }
}
