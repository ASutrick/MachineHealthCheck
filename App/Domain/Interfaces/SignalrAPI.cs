using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Interfaces
{
    public static class SignalrAPI
    {
        public static string HubUrl => "https://localhost:7296/hubs/mhc";

        public static class Events
        {
            public static string VerifyKey => nameof(ISignalrHub.VerifyKey);
            public static string HealthCheckRequest => nameof(ISignalrHub.HealthCheckRequest);
            public static string HealthCheckResponse => nameof(ISignalrHub.HealthCheckResponse);
        }
    }
}
