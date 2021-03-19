using System;

namespace Domain.Entities
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
        }

        public Guid Id { get; }
    }
}