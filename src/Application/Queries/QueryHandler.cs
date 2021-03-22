using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
        private readonly ILogger<QueryHandler<TQuery, TResult>> _logger;

        protected QueryHandler(ILogger<QueryHandler<TQuery, TResult>> logger)
        {
            _logger = logger;
        }

        public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"[PROCESS QUERY]-[{typeof(TQuery).FullName}]");
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var result = await Process(query);
            
            stopwatch.Stop();
            
            _logger.Log(LogLevel.Information, $"[QUERY PROCESSED]-[{typeof(TQuery).FullName}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
            return result;
        }

        protected abstract Task<TResult> Process(TQuery query);
    }
}