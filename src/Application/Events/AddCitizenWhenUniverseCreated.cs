using System;
using System.Threading.Tasks;
using Application.Services;
using Application.Transactions;
using Domain;
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;
using MassTransit;

namespace Application.Events
{
    public class AddCitizenWhenUniverseCreated : IConsumer<UniverseCreated>
    {
        private readonly ICitizenRepository _citizenRepository;
        private readonly INameGenerator _nameGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AddCitizenWhenUniverseCreated(IUnitOfWork unitOfWork, ICitizenRepository citizenRepository, INameGenerator nameGenerator)
        {
            _unitOfWork = unitOfWork;
            _citizenRepository = citizenRepository;
            _nameGenerator = nameGenerator;
        }

        public async Task Consume(ConsumeContext<UniverseCreated> context)
        {
            const int amountOfCouples = 4;
            var universeCreated = context.Message;

            for (var i = 0; i < amountOfCouples; i++)
            {
                var maleName = _nameGenerator.GenerateName(Gender.Male);
                var maleCitizen = new Citizen(Guid.NewGuid(), Gender.Male, maleName, universeCreated.CurrentTime);
                await _citizenRepository.Add(maleCitizen);

                var femaleName = _nameGenerator.GenerateName(Gender.Female);
                var femaleCitizen = new Citizen(Guid.NewGuid(), Gender.Female, femaleName, universeCreated.CurrentTime);
                await _citizenRepository.Add(femaleCitizen);
            }

            await _unitOfWork.Commit();
        }
    }
}