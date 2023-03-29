using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MachineHealthCheck.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineInfoController : Controller
    {
        private readonly ILogger<MachineInfoController> _logger;
        private readonly IMachineInfoService _machineInfoService;

        public MachineInfoController(ILogger<MachineInfoController> logger, IMachineInfoService machineInfoService)
        {
            _logger = logger;
            _machineInfoService = machineInfoService;
        }
        [HttpGet(Name = "ListMachineInfos")]
        public async Task<IList<MachineInfo>> ListMachineInfos()
        {
            return await _machineInfoService.GetAll();
        }
    }
}
