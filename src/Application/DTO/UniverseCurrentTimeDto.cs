using System;

namespace Application.DTO
{
    public class UniverseCurrentTimeDto
    {
        public UniverseCurrentTimeDto(DateTime currentTime)
        {
            CurrentTime = currentTime;
        }

        public DateTime CurrentTime { get; }
    }
}