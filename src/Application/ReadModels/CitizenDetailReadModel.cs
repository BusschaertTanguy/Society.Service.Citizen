using System;
using Domain;

namespace Application.ReadModels
{
    public class CitizenDetailReadModel
    {
        public CitizenDetailReadModel(Guid id, Gender gender, string name, DateTime dateOfBirth)
        {
            Id = id;
            Gender = gender;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public DateTime DateOfBirth { get; }
        public Gender Gender { get; }
        public Guid Id { get; }
        public string Name { get; }
    }
}