using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace HealthCheck.Host.Services
{
    public class WorkQueueBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<SignalrHub,ISignalrHub> _hub;
        private readonly ILogger<WorkQueueBackgroundService> _logger;

        public WorkQueueBackgroundService(IServiceProvider serviceProvider, IHubContext<SignalrHub, ISignalrHub> hub, ILogger<WorkQueueBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _hub=hub;
            _logger=logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                await CheckQueue();
                await Task.Delay(3000);
            }
        }
        private async Task CheckQueue()
        {
            using(IServiceScope scope = _serviceProvider.CreateScope())
            {
                IWorkQueueService? workQueueService = scope.ServiceProvider.GetService<IWorkQueueService>();
                WorkQueue? work = await workQueueService!.DequeueWork();
                if (work != null)
                {
                    _logger.LogInformation($"Sending request to Id {work.Id}");
                    _hub.Clients.Client(work.ConnectionId).HealthCheckRequest(DateTime.Now.ToString());
                }
            }
            
        }
    }
}
