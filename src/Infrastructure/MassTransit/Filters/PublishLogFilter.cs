using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MassTransit.Filters
{
    public class PublishLogFilter<T> : IFilter<PublishContext<T>> where T : class
    {
        private readonly ILogger<PublishLogFilter<T>> _logger;

        public PublishLogFilter(ILogger<PublishLogFilter<T>> logger)
        {
            _logger = logger;
        }

        public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            _logger.Log(LogLevel.Information, $"[START PUBLISHING]-[{context.Message.GetType().Name}]");
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            await next.Send(context);
            
            stopwatch.Stop();
            
            _logger.Log(LogLevel.Information, $"[DONE PUBLISHING]-[{context.Message.GetType().Name}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("publishLog");
        }
    }
}