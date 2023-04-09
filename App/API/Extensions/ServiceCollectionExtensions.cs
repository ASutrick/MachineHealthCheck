using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MachineHealthCheck.Domain;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Infrastructure;
using MachineHealthCheck.Service;
using MachineHealthCheck.Domain.Models;
using MachineHealthCheck.Domain.Interfaces.Services;

namespace MachineHealthCheck.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add needed instances for database
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime   
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

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
                services.AddCors(options => {
                    options.AddPolicy("CorsPolicy",
                        builder => {
                            builder.WithOrigins(AppSettings.CORS)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                });
        }
    }
}
