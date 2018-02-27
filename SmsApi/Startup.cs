using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Serilog;
using Serilog.Events;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SmsApi.Models;

namespace SmsApi
{
    public class Startup
    {
        private IHostingEnvironment CurrentEnvironment { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{CurrentEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            // "Server=LAP10123;Database=SmsApi;Trusted_Connection=True;ConnectRetryCount=0"
            // In order for our MVC controllers to make use of ApiDbContext register it as a service.
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));

            services.AddMvc();

            services.AddSingleton<Serilog.ILogger>
            (x => new LoggerConfiguration()
                  .MinimumLevel.Information()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                  .MinimumLevel.Override("System", LogEventLevel.Error)
                  .ReadFrom.Configuration(configuration)
                  .CreateLogger()
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SMS API API", Version = "v1" });
            });
        }

        // Scaffold-DbContext "Server=SRV003459\SPFWEB;Database=EmployeeInformation;User ID=sqlrecognition;Password=expry4qq;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS API V1");
            });

            app.UseMvc();
        }
    }
}
