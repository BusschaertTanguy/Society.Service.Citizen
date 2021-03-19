using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Transactions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands
{
    public static class CreateCitizen
    {
        public class Command : IRequest
        {
            public Command(Guid id, string name)
            {
                Id = id;
                Name = name;
            }

            public Guid Id { get; }
            public string Name { get; }
        }

        internal class Handler : IRequestHandler<Command>
        {
            private readonly ICitizenRepository _citizenRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork, ICitizenRepository citizenRepository)
            {
                _unitOfWork = unitOfWork;
                _citizenRepository = citizenRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var citizen = new Citizen(request.Id, request.Name);
                await _citizenRepository.Add(citizen);
                await _unitOfWork.Commit();
                return Unit.Value;
            }
        }
    }
}