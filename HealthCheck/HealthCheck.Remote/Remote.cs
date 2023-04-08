using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MachineHealthCheck.Remote.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MachineHealthCheck.Remote
{
    public class Remote
    {
        public static void Main(string[] args)
        {
            string key;
            if(args.Length == 0) {
                Console.WriteLine("Please enter the key provided to you:");
                key = Console.ReadLine();
                if(String.IsNullOrEmpty(key)) { Environment.Exit(1); }
            }
            else
            {
                key = args[0];
            }
           
            var host = new HostBuilder()
                .ConfigureAppConfiguration(configApp =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices(services =>
                { 
                    services.AddSingleton(new ServiceArgs(key));
                    services.AddHostedService<SignalrClientService>();
                })
                .ConfigureLogging(configLogging =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .Build();

            host.Run();
        }
    }
}
