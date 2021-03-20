using System;

namespace Domain.Events
{
    public class UniverseCreated
    {
        public UniverseCreated(Guid id, DateTime currentTime)
        {
            Id = id;
            CurrentTime = currentTime;
        }

        public DateTime CurrentTime { get; }
        public Guid Id { get; }
    }
}