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

        public event Action<NewDayEvent> OnNewDay;
        public event Action<DayCycleChangedEvent> OnDayCycleChanged;
        public event Action<NewHourEvent> OnNewHour;
        private int _lastHour;

        public DayCycle(DayCycleDto cycleDto)
        {
            CurrentTime = cycleDto.Time;
            DayLength = cycleDto.DayLength;
            _lastHour = GetHour();
        }

        public void UpdateTime(object sender, float deltaTime)
        {

            CurrentTime += deltaTime;
            if (CurrentTime >= DayLength)
            {
                CurrentTime = 0;
                var newDayEvent = new NewDayEvent(sender, GetDto());
                OnNewDay?.Invoke(newDayEvent);
            }
            
            var dayCycleChangedEvent = new DayCycleChangedEvent(sender, GetDto());
            OnDayCycleChanged?.Invoke(dayCycleChangedEvent);
            
            int newHour = GetHour();
            if (newHour != _lastHour)
            {
                var newHourEvent = new NewHourEvent(sender, GetDto(), newHour);
                OnNewHour?.Invoke(newHourEvent);
            }

            _lastHour = newHour;
        }

        public DayCycleDto GetDto()
        {
            return new DayCycleDto
            {
                Time = CurrentTime,
                TimeOfDay = GetTimeOfDay(),
                DayLength = DayLength,
            };
        }

        public float GetNormalizedTime()
        {
            if (DayLength == 0f) return 0f;
            return CurrentTime / DayLength;
        }

        public (int hour, int minute) GetTime24()
        {
            float totalTime = GetNormalizedTime() * 24f;
            int hour = (int) Math.Floor(totalTime);
            float fractional = totalTime - hour;
            int minute = (int)Math.Floor(fractional * 60f);
            return (hour, minute);
        }

        private static readonly List<(TimeOfDay Period, float Start, float End)> Periods = new()
        {
            (TimeOfDay.Night, 0f, 0.25f),
            (TimeOfDay.Morning, 0.25f, 0.5f),
            (TimeOfDay.Day, 0.5f, 0.75f),
            (TimeOfDay.Evening, 0.75f, 1f)
        };

        public void SetTimeOfDay(object sender, TimeOfDay timeOfDay)
        {
            var period = Periods.FirstOrDefault(p => p.Period == timeOfDay);
            if (period == default)
            {
                throw new ArgumentException($"Invalid TimeOfDay: {timeOfDay}");
            }
            float normalized = (period.Start + period.End) / 2f;
            CurrentTime = normalized * DayLength;

            var dayCycleChangedEvent = new DayCycleChangedEvent(sender, GetDto());
            OnDayCycleChanged?.Invoke(dayCycleChangedEvent);
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

        private int GetHour()
        {
            int hour = (int) Math.Floor(GetNormalizedTime() * 24f);
            return hour;
        }
    }
}