using Microsoft.EntityFrameworkCore;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Infrastructure;
using MachineHealthCheck.Service;
using MachineHealthCheck.Domain.Models;
using MachineHealthCheck.Domain.Interfaces.Services;

namespace MachineHealthCheck.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(AppSettings.ConnectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120));
            }
            );
            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMachineInfoService, MachineInfoService>();
            services.AddScoped<IWorkQueueService, WorkQueueService>();
            services.AddScoped<IHealthCheckService, HealthCheckService>();
            return services;

        }

        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            return // CORS
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });
        }
    }
}
