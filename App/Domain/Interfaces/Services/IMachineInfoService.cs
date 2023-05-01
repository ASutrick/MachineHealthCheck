using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Models;

namespace MachineHealthCheck.Domain.Interfaces
{
    public interface IMachineInfoService
    { 
        Task<IList<MachineInfo>> GetAll();
        Task<IList<MachineInfo>> GetAllActive();
        Task<bool> KeyExists(string key); 
        Task<MachineInfo> GetOne(Guid machineId);
        Task<MachineInfoDTO> Update(string key, MachineInfoDTO machine);
        Task Add(MachineInfo machine);
        Task Delete(string key);
    }
}
