using MachineHealthCheck.Domain.Interfaces.Services;
using MachineHealthCheck.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace SignalR.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerificationController : ControllerBase
    {

        private readonly ILogger<VerificationController> _logger;
        private readonly IVerificationService _verificationService;
        public VerificationController(ILogger<VerificationController> logger, IVerificationService verificationService)
        {
            _verificationService = verificationService;
            _logger = logger;
        }

        [HttpPut(Name = "VerifyKey")]
        public async Task<ActionResult<bool>> VerifyKey(string key)
        {
            return await _verificationService.Verify(key);  
        }
    }
}