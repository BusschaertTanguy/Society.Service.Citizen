using System;

namespace Domain.Events
{
    public class DayPassed
    {
        public DayPassed(Guid universeId, DateTime newTime)
        {
            UniverseId = universeId;
            NewTime = newTime;
        }

        public DateTime NewTime { get; }

        public Guid UniverseId { get; }
    }
}