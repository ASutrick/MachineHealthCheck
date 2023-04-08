using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace HealthCheck.Host
{
    public class SignalrHub : Hub<ISignalrHub>
    {
        private readonly ILogger<SignalrHub> _logger;
        private readonly IHealthCheckHubService _hubService;
        public SignalrHub(ILogger<SignalrHub> logger, IHealthCheckHubService hubService)
        {
            _hubService = hubService;
            _logger = logger;
        }
        public async Task VerifyKey(string key)
        {
            bool success = await _hubService.Verify(key, Context.ConnectionId);
            if (!success)
            {
                _logger.LogInformation($"A key failed verification {key}");
                return;
            }
            _logger.LogInformation($"Key verified: {key}");
            return;
        }
        public Task HealthCheckRequest(string date)
        {
            throw new NotImplementedException();
        }
        public Task HealthCheckResponse(MachineInfo info, string key)
        {
            throw new NotImplementedException();
        }



    }
}
