using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Events
{
    public abstract class EventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : INotification
    {
        private readonly ILogger<EventHandler<TEvent>> _logger;

        protected EventHandler(ILogger<EventHandler<TEvent>> logger)
        {
            _logger = logger;
        }

        public async Task Handle(TEvent @event, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"[PROCESS EVENT]-[{typeof(TEvent).FullName}]");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await Process(@event);

            stopwatch.Stop();

            _logger.Log(LogLevel.Information, $"[EVENT PROCESSED]-[{typeof(TEvent).FullName}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
        }

        protected abstract Task Process(TEvent @event);
    }
}