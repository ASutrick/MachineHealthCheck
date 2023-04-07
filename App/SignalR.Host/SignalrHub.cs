using HealthCheck.Host;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalR.Host
{
    public class SignalrHub : Hub
    {
        public static ConcurrentDictionary<string, Client> MyClients = new ConcurrentDictionary<string,Client>();
        private readonly ILogger<SignalrHub> _logger;
        public SignalrHub(ILogger<SignalrHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            MyClients.TryAdd(Context.ConnectionId, new Client() { ConnId = Context.ConnectionId });
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Client garbage;
            MyClients.TryRemove(Context.ConnectionId, out garbage);
            return base.OnDisconnectedAsync(exception);
        }
        public Task ConnectInfo(string key)
        {
            return Task.CompletedTask;
        }



    }
}
