﻿using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MachineHealthCheck.Remote.Services
{
    internal class SignalrClientService : IHostedService
    {
        private HubConnection _connection;
        private readonly ILogger<SignalrClientService> _logger;
        private readonly string _key;
        public SignalrClientService(ILogger<SignalrClientService> logger, ServiceArgs args)
        {
            _key = args.Key;
            _logger = logger;
            _connection = new HubConnectionBuilder()
                .WithUrl(SignalrAPI.HubUrl)
                .Build();
            _connection.On<string>(SignalrAPI.Events.HealthCheckRequest, HealthCheckRequest);
        }
        public Task HealthCheckResponse(MachineInfo info, string machineInfo)
        {
            throw new NotImplementedException();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);
                    _logger.LogInformation("Connected to host");
                    break;
                }
                catch
                {
                    _logger.LogInformation("Failed Connection, retrying...");
                    await Task.Delay(1000, cancellationToken);
                }
            }
            await _connection.InvokeAsync(SignalrAPI.Events.VerifyKey, _key);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.DisposeAsync();
        }
        public Task HealthCheckRequest(string date)
        {
            _logger.LogInformation("Request Recieved: Performing Health Check");
            SystemInfoRetrievalService infoService = new SystemInfoRetrievalService();
            HealthCheck info = infoService.GetInfo();
            info.Date = DateTime.Parse(date);
            _connection.InvokeAsync(SignalrAPI.Events.HealthCheckResponse, info, _key);
            _logger.LogInformation("Health Check Complete!");
            return Task.CompletedTask;
        }
    }
}
