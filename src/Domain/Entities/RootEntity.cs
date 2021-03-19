using System;

namespace Domain.Entities
{
    public abstract class RootEntity : Entity
    {
        protected RootEntity(Guid id) : base(id)
        {
        }
    }
}