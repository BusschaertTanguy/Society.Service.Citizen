using System;

namespace Application.ReadModels
{
    internal class CitizenReadModel
    {
        public CitizenReadModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}