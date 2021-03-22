using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Transactions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : IRequest
    {
        private readonly ILogger<CommandHandler<TCommand>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        protected CommandHandler(ILogger<CommandHandler<TCommand>> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"[PROCESS COMMAND]-[{typeof(TCommand).FullName}]");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await Process(command);
            await _unitOfWork.Commit();

            stopwatch.Stop();

            _logger.Log(LogLevel.Information, $"[COMMAND PROCESSED]-[{typeof(TCommand).FullName}]-[TIME: {stopwatch.ElapsedMilliseconds} ms]");
            return Unit.Value;
        }

        protected abstract Task Process(TCommand command);
    }
}