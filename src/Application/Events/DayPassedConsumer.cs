using System;
using System.Threading.Tasks;
using Domain.Events;
using MassTransit;

namespace Application.Events
{
    public class DayPassedConsumer : IConsumer<DayPassed>
    {
        public Task Consume(ConsumeContext<DayPassed> context)
        {
            Console.WriteLine($"[DAY PASSED]: Time: {context.Message.NewTime}");
            return Task.CompletedTask;
        }
    }
}