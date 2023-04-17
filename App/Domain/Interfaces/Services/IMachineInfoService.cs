using MachineHealthCheck.Domain.Entities;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface IMachineInfoService
    { 
        Task<IList<MachineInfo>> GetAll();
        Task<IList<MachineInfo>> GetAllActive();
        Task<MachineInfo> GetOne(Guid machineId);
        Task Update(MachineInfo machine);
        Task Add(MachineInfo machine);
        Task Delete(MachineInfo machine);
    }
}
