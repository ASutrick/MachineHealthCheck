using Microsoft.OpenApi.Models;
using MachineHealthCheck.API.Extensions;
using MachineHealthCheck.Domain.Models;
namespace MachineHealthCheck.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            Configuration = InitConfiguration(env);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase()
                .AddServices()
                .AddCORS();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MachineHealthCheck.API", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Move swagger out of this if block if use want to use it on production
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationTier.API v1"));
            }

            // Auto redirect to https
            //app.UseHttpsRedirection();
            // Allow external access
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private IConfiguration InitConfiguration(IWebHostEnvironment env)
        {
            // Config the app to read values from appsettings base on current environment value.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables().Build();
            //
            // Map AppSettings section in appsettings.json file value to AppSetting model
            configuration.GetSection("AppSettings").Get<AppSettings>(options => options.BindNonPublicProperties = true);
            return configuration;
        }

    }
}
