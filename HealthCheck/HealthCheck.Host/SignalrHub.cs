using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

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
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            bool success = await _hubService.Disconnect(Context.ConnectionId);
            if (!success)
            {
                _logger.LogError($"Machine with connection id {Context.ConnectionId} disconnected and it's connection Id may be wrong");
                return;
            }
            _logger.LogInformation($"Machine Disconnected with connection Id: {Context.ConnectionId}");
            return;
        }
        public async Task VerifyKey(string key)
        {
            bool success = await _hubService.Verify(key, Context.ConnectionId);
            if (!success)
            {
                _logger.LogError($"A key failed verification {key}");
                return;
            }
            _logger.LogInformation($"Key verified: {key}");
            return;
        }
        public Task HealthCheckRequest(string date)
        {
            throw new NotImplementedException();
        }
        public async Task HealthCheckResponse(MachineHealthCheck.Domain.Entities.HealthCheck info, string key)
        {
            bool success = await _hubService.AddHealthCheck(info, key);
            if (!success)
            {
                _logger.LogError($"Failed to add health check for machine with key: {key}");
                return;
            }
            _logger.LogInformation($"New Health Check for machine with key: {key}");
            return;
        }
    }
}
