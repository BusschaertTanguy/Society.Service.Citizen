using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MassTransit.Filters
{
    public class SendLogFilter<T> : IFilter<SendContext<T>> where T : class
    {
        private readonly ILogger<SendLogFilter<T>> _logger;

        public SendLogFilter(ILogger<SendLogFilter<T>> logger)
        {
            _logger = logger;
        }

        public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            _logger.Log(LogLevel.Information, $"[START SENDING]-[{context.Message.GetType().Name}]");
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            await next.Send(context);
            
            stopwatch.Stop();
            
            _logger.Log(LogLevel.Information, $"[DONE SENDING]-[{context.Message.GetType().Name}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("sendLog");
        }
    }
}