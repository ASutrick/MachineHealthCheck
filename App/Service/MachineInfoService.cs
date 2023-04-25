using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            MachineInfo? m;
            IQueryable<MachineInfo> info = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == machine.Key && m.isActive);
            try
            {
                m = info.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return;
            }
            if (m == null) 
            {
                machine.isActive = true;
                await _unitOfWork.Repository<MachineInfo>().InsertAsync(machine, true);
            }
            return;
           
        }

        public async Task Delete(string key)
        {
            MachineInfo m;
            IQueryable<MachineInfo> info = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key && m.isActive);
            try
            {
                m = info.First();
            }
            catch (Exception ex)
            {
                
                return;
            }
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();
                work.isActive = false;
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<MachineInfo>> GetAll()
        {
            return await _unitOfWork.Repository<MachineInfo>().GetAllAsync();
        }
        public async Task<IList<MachineInfo>> GetAllActive()
        {
            var actives = await _unitOfWork.Repository<MachineInfo>().FindByCondition(machine => machine.isActive);
            return actives.ToList();
        }
        public async Task<MachineInfo> GetOne(Guid machineId)
        {
            return await _unitOfWork.Repository<MachineInfo>().FindAsync(machineId);
        }
        public async Task<MachineInfoDTO> Update(string key ,MachineInfoDTO machine)
        {
            MachineInfo m;
            IQueryable<MachineInfo> info = await _unitOfWork.Repository<MachineInfo>().FindByCondition(m => m.Key == key);
            try
            {
                m = info.First();
            }
            catch (Exception ex)
            {

                return null;
            }
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<MachineInfo>();
                var work = await workRepos.FindAsync(m.Id);
                if (work == null)
                    throw new KeyNotFoundException();
                work.ClientName = machine.Name;
                work.MachineName = machine.Machine;
                work.LastChecked = machine.LastChecked;
                work.Key = machine.Key;
                work.isVerified = machine.IsVerified;
                await _unitOfWork.CommitTransaction();
                machine = MachineInfoDTO.FromMI(work);
                return machine;

            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}