using System;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Services
{
    public class ConsulService : IHostedService
    {
        private readonly IServiceProvider _provider;
        private string _registrationId;

        public ConsulService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = Guid.NewGuid().ToString();

            using var scope = _provider.CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = configuration["Consul:Name"],
                Address = configuration["Consul:Host"],
                Port = int.Parse(configuration["Consul:Port"])
            };

            var client = scope.ServiceProvider.GetRequiredService<IConsulClient>();
            await client.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await client.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();
            var client = scope.ServiceProvider.GetRequiredService<IConsulClient>();
            await client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}