using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MachineHealthCheck.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineInfoController : Controller
    {
        private readonly ILogger<MachineInfoController> _logger;
        private readonly IMachineInfoService _machineInfoService;

        public MachineInfoController(ILogger<MachineInfoController> logger, IMachineInfoService machineInfoService)
        {
            _logger = logger;
            _machineInfoService = machineInfoService;
        }

        [HttpGet("ListMachineInfos")]
        public async Task<ActionResult<IList<MachineInfo>>> ListMachineInfos()
        {
            var list = await _machineInfoService.GetAll();

            if (list == null)
            {
                return (ActionResult<IList<MachineInfo>>)NotFound();
            }

            return (ActionResult<IList<MachineInfo>>)Ok(list);
        }
    }
}
