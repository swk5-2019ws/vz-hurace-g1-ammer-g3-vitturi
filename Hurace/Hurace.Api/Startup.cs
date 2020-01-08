using FluentValidation;
using FluentValidation.AspNetCore;
using Hurace.Api.Helper;
using Hurace.Core;
using Hurace.Core.Daos;
using Hurace.Core.Interface.Services;
using Hurace.Core.Services;
using Hurace.Domain;
using Hurace.Simulator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hurace.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "Hurace API", Version = "v1"}));

            services.AddTransient<IValidator<Skier>, SkierValidator>();
            AddHuraceServices(services);
        }

        private void AddHuraceServices(IServiceCollection services)
        {
            var connectionString = ConfigurationReader.GetConnectionString(Environment.Production);
            var connectionFactory = new ConnectionFactory(Environment.Production, connectionString);
            var locationDao = new LocationDao(connectionFactory);
            var skierDao = new SkierDao(connectionFactory);
            var countryDao = new CountryDao(connectionFactory);
            var raceDao = new RaceDao(connectionFactory);
            var runDao = new RunDao(connectionFactory);
            var sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);
            var daoProvider = new DaoProvider(countryDao, locationDao, raceDao, runDao, sensorMeasurementDao, skierDao);

            services.AddSingleton<IRaceService>(new RaceService(daoProvider));
            services.AddSingleton<ISkierService>(new SkierService(daoProvider));
            services.AddSingleton<ILocationService>(new LocationService(daoProvider));
            services.AddSingleton<IRunService>(new RunService(daoProvider, new SimulatorRaceClock()));
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.WithOrigins("*").WithMethods("GET", "POST", "PUT", "DELETE").AllowAnyHeader()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hurace v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}