using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Models;
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
        public async Task<ActionResult<IList<MachineInfoDTO>>> ListMachineInfos()
        {
            List<MachineInfoDTO> returns = new List<MachineInfoDTO>();
            var list = await _machineInfoService.GetAll();

            if (list == null)
            {
                return (ActionResult<IList<MachineInfoDTO>>)NotFound();
            }
            foreach (var item in list)
            {
                returns.Add(MachineInfoDTO.FromMI(item));
            }

            return (ActionResult<IList<MachineInfoDTO>>)Ok(returns);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateMachineInfo([FromBody]MachineInfoDTO m)
        {
            var item = m.ToMI();
            try
            {
                await _machineInfoService.Add(item);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            
        }
    }
}
