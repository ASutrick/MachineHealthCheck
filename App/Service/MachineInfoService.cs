using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
namespace MachineHealthCheck.Service
{
    public class MachineInfoService : IMachineInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MachineInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(MachineInfo machine)
        {
            await _unitOfWork.Repository<MachineInfo>().InsertAsync(machine, true);
        }

        public async Task Delete(MachineInfo machine)
        {
            await _unitOfWork.Repository<MachineInfo>().DeleteAsync(machine, true);
        }

        public async Task<IList<MachineInfo>> GetAll()
        {
            return await _unitOfWork.Repository<MachineInfo>().GetAllAsync();
        }

        public async Task<MachineInfo> GetOne(Guid machineId)
        {
            return await _unitOfWork.Repository<MachineInfo>().FindAsync(machineId);
        }

        public async Task Update(MachineInfo machine)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(machine.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}