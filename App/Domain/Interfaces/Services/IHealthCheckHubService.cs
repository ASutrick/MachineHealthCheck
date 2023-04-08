﻿using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Interfaces.Services
{
    public interface IHealthCheckHubService
    {
        Task<bool> Verify(string key, string connId);
        Task<bool> Disconnect(string connId);
        Task<bool> AddHealthCheck(HealthCheck healthCheck, string key);
    }
}
