using System;
using Application.Services;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Citizen WebApi",
                    Version = "v1"
                });

                c.CustomSchemaIds(type => type.ToString());
            });

            services.ConfigureDatabase(Configuration);
            services.ConfigureCitizenServices();
            services.ConfigureMassTransit(Configuration);

            services.AddHttpClient<IUniverseService, UniverseService>(client =>
            {
                var endpoint = $"http://{Configuration["Services:Universe"]}/api/";
                client.BaseAddress = new Uri(endpoint);
            });

            services.AddCors(option => option.AddPolicy("All", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Citizen WebApi v1"); });
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors("All");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}