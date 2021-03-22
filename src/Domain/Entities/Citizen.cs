using System;

namespace Domain.Entities
{
    public class Citizen : RootEntity
    {
        private readonly DateTime _dateOfBirth;
        private readonly Gender _gender;
        private readonly string _name;

        public Citizen(Guid id, Gender gender, string name, DateTime dateOfBirth) : base(id)
        {
            _gender = gender == Gender.None ? throw new ArgumentNullException(nameof(gender)) : gender;
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _dateOfBirth = dateOfBirth;
        }
    }
}