using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces.Services;
using MachineHealthCheck.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineHealthCheck.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkQueueController : Controller
    {
        IWorkQueueService _workQueueService;
        public WorkQueueController(IWorkQueueService workQueueService)
        {
            _workQueueService = workQueueService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Queue(string key)
        {
            try
            {
                await _workQueueService.QueueWork(key);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<WorkQueue>> Dequeue()
        {
            WorkQueue? work;
            try
            {
                work = await _workQueueService.DequeueWork();
            }
            catch (Exception ex)
            {
                return BadRequest("There may be no work queued");
            }
            if (work == null) return BadRequest();

            return (ActionResult<WorkQueue>)Ok(work);
        }
    }
    
}
