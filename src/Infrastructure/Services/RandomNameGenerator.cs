using System;
using Application.Services;
using Domain;
using RandomNameGeneratorLibrary;

namespace Infrastructure.Services
{
    internal class RandomNameGenerator : INameGenerator
    {
        private readonly PersonNameGenerator _generator;

        public RandomNameGenerator()
        {
            _generator = new PersonNameGenerator();
        }

        public string GenerateName(Gender gender)
        {
            return gender switch
            {
                Gender.None => _generator.GenerateRandomFirstAndLastName(),
                Gender.Male => _generator.GenerateRandomMaleFirstAndLastName(),
                Gender.Female => _generator.GenerateRandomFemaleFirstAndLastName(),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }
    }
}