using Application.Configurations;
using Application.Connections;
using Application.Events;
using Application.Transactions;
using Domain.Repositories;
using Infrastructure.EntityFramework.Contexts;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Transactions;
using Infrastructure.Queries;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CitizenDb");
            services.AddDbContext<CitizenDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDbConnectionProvider>(_ => new SqlDbConnectionProvider(connectionString));
        }

        public static void ConfigureCitizenServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationLayerConfiguration).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICitizenRepository, CitizenRepository>();
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var username = configuration["MassTransit:Username"];
            var password = configuration["MassTransit:Password"];
            var host = configuration["MassTransit:Host"];

            var universeEndpoint = configuration["QueueEndpoints:Universe"];

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((_, configurator) =>
                {
                    configurator.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    configurator.ReceiveEndpoint(universeEndpoint, e =>
                    {
                        e.Consumer<UniverseCreatedConsumer>();
                        e.Consumer<DayPassedConsumer>();
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}