using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace HealthCheck.Host
{
    public class SignalrHub : Hub<ISignalrHub>
    {
        public static ConcurrentDictionary<string, ClientModel> MyClients = new ConcurrentDictionary<string,ClientModel>();
        private readonly ILogger<SignalrHub> _logger;
        private readonly IHealthCheckHubService _hubService;
        public SignalrHub(ILogger<SignalrHub> logger, IHealthCheckHubService hubService)
        {
            _hubService = hubService;
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected {Context.ConnectionId}");
            MyClients.TryAdd(Context.ConnectionId, new ClientModel() { ConnId = Context.ConnectionId });
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ClientModel garbage;
            MyClients.TryRemove(Context.ConnectionId, out garbage);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task VerifyKey(string key)
        {
            bool success = await _hubService.Verify(key);
            if (!success)
            {
                _logger.LogInformation($"A key failed verification {key}");
                return;
            }
            _logger.LogInformation($"Key verified: {key}");
            MyClients[Context.ConnectionId] = new ClientModel() { ConnId = Context.ConnectionId, Key = key };
            return;
        }
        public Task HealthCheckRequest(string info, string key)
        {
            throw new NotImplementedException();
        }
        public Task HealthCheckResponse(MachineInfo info, string key)
        {
            throw new NotImplementedException();
        }



    }
}
