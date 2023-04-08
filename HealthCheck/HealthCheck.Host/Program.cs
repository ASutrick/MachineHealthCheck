using MachineHealthCheck.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;

namespace HealthCheck.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"))
                        .Build();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;TrustServerCertificate=Yes;Database=MachineHealthCheck;Trusted_Connection=True;",
                    sqlOptions => sqlOptions.CommandTimeout(120));
            }
            );

            builder.Services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            builder.Services.AddScoped<DbFactory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add services to the container.
            builder.Services.AddScoped<IVerificationService, VerificationService>();
            //builder.Services.AddHostedService<SignalrWorkerService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<SignalrHub>("/hubs/mhc");
            app.Run();
        }
    }
}
