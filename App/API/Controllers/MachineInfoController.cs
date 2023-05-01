using MachineHealthCheck.Domain.Entities;
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
            IList<MachineInfo> list;
            try
            {
                list = await _machineInfoService.GetAllActive();
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
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateMachineInfo([FromBody]MachineInfoDTO m)
        {
            var item = m.ToMI();
            try
            {
                bool keyExists = await _machineInfoService.KeyExists(m.Key);
                if(!keyExists)
                {
                    await _machineInfoService.Add(item);
                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }           
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteMachineInfo(string key)
        {
            try
            {
                await _machineInfoService.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update")]
        public async Task<ActionResult<MachineInfoDTO>> UpdateMachineInfo(string key,[FromBody] MachineInfoDTO m)
        {
            try
            {
                MachineInfoDTO? updated = await _machineInfoService.Update(key,m);
                if(updated != null)
                {
                    return (ActionResult<MachineInfoDTO>)Ok(updated);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
