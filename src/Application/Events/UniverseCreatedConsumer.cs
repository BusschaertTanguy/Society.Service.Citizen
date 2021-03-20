using System;
using System.Threading.Tasks;
using Domain.Events;
using MassTransit;

namespace Application.Events
{
    public class UniverseCreatedConsumer : IConsumer<UniverseCreated>
    {
        public Task Consume(ConsumeContext<UniverseCreated> context)
        {
            Console.WriteLine($"[UNIVERSE CREATED]: Time: {context.Message.CurrentTime}");
            return Task.CompletedTask;
        }
    }
}