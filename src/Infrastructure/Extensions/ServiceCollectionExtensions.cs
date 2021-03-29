using System;
using Application.Configurations;
using Application.Events;
using Application.Queries;
using Application.Services;
using Application.Transactions;
using Consul;
using Domain.Repositories;
using Infrastructure.Dapper.Queries;
using Infrastructure.EntityFramework.Contexts;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Transactions;
using Infrastructure.MassTransit.Filters;
using Infrastructure.Queries;
using Infrastructure.Services;
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
            services.AddScoped(_ => new SqlDbConnectionProvider(connectionString));
        }

        public static void ConfigureCitizenServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationLayerConfiguration).Assembly);
            services.AddSingleton<INameGenerator, RandomNameGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICitizenRepository, CitizenRepository>();
            services.AddTransient<ICitizenQueries, CitizenQueries>();
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var username = configuration["MassTransit:Username"];
            var password = configuration["MassTransit:Password"];
            var host = configuration["MassTransit:Host"];

            var universeEndpoint = configuration["QueueEndpoints:Universe"];

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<AddCitizenWhenUniverseCreated>();

                cfg.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    configurator.UseConsumeFilter(typeof(ConsumerLogFilter<>), context);
                    configurator.UseSendFilter(typeof(SendLogFilter<>), context);
                    configurator.UsePublishFilter(typeof(PublishLogFilter<>), context);

                    configurator.ReceiveEndpoint(universeEndpoint, e => { e.Consumer<AddCitizenWhenUniverseCreated>(context); });
                });
            });

            services.AddMassTransitHostedService();
        }

        public static void ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            var consulClient = new ConsulClient(clientConfiguration => { clientConfiguration.Address = new Uri(configuration["Consul:Discovery"]); });
            services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);
        }
    }
}