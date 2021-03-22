using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MassTransit.Filters
{
    public class ConsumerLogFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly ILogger<ConsumerLogFilter<T>> _logger;

        public ConsumerLogFilter(ILogger<ConsumerLogFilter<T>> logger)
        {
            _logger = logger;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            _logger.Log(LogLevel.Information, $"[START CONSUMING]-[{context.Message.GetType().FullName}]");
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            await next.Send(context);
            
            stopwatch.Stop();
            
            _logger.Log(LogLevel.Information, $"[DONE CONSUMING]-[{context.Message.GetType().FullName}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("consumerLog");
        }
    }
}