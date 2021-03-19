using Application.Configurations;
using Application.Connections;
using Application.Transactions;
using Domain.Repositories;
using Infrastructure.EntityFramework.Contexts;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Transactions;
using Infrastructure.Queries;
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
    }
}