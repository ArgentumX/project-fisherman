using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models.Common;
using Domain.Models.Entities.DayCycle.Events;

namespace Domain.Models.Entities.DayCycle
{
    public class DayCycle : BaseModel
    {
        public float CurrentTime { get; private set; }
        public float DayLength { get; private set; }

        public TimeOfDay TimeOfDay => GetTimeOfDay();
        
        public DayCycle(DayCycleState cycleState)
        {
            CurrentTime = cycleState.Time;
            DayLength = cycleState.DayLength;
        }
        public void UpdateTime(object sender, float deltaTime)
        {
            CurrentTime += deltaTime;
            if (CurrentTime >= DayLength) {
                CurrentTime = 0;
                AddDomainEvent(new NewDayEvent(sender, GetState()));
            }
            AddDomainEvent(new DayCycleChangedEvent(sender, GetState()));
        }

        public DayCycleState GetState()
        {
            return new DayCycleState
            {
                Time = CurrentTime,
                TimeOfDay = GetTimeOfDay(),
                DayLength = DayLength,
            };
        }
        public float GetNormalizedTime()
        {
            return CurrentTime / DayLength;
        }
        private static readonly List<(TimeOfDay Period, float Start, float End)> Periods = new()
        {
            (TimeOfDay.Night, 0f, 0.25f),
            (TimeOfDay.Morning, 0.25f, 0.5f),
            (TimeOfDay.Day, 0.5f, 0.75f),
            (TimeOfDay.Evening, 0.75f, 1f)
        };
        public void SetTimeOfDay(TimeOfDay timeOfDay)
        {
            var period = Periods.FirstOrDefault(p => p.Period == timeOfDay);
            if (period == default) {
                throw new ArgumentException($"Invalid TimeOfDay: {timeOfDay}");
            }
            float normalized = (period.Start + period.End) / 2f;
            CurrentTime = normalized * DayLength;
            AddDomainEvent(new DayCycleChangedEvent(null, GetState()));
        }
        
        private TimeOfDay GetTimeOfDay()
        {
            float normalized = GetNormalizedTime();
            foreach (var period in Periods)
            {
                if (normalized >= period.Start && normalized < period.End)
                {
                    return period.Period;
                }
            }
            return TimeOfDay.Night;
        }
    }
}