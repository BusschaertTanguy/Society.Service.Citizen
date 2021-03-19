using System;

namespace Domain.Entities
{
    public class Citizen : RootEntity
    {
        private readonly string _name;
        
        public Citizen(Guid id, string name) : base(id)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}