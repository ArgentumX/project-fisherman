using Domain.Enums;
using Domain.Models.Common;
using Domain.Models.Entities.DayCycle.Events;

namespace Domain.Models.Entities.DayCycle
{
    public class DayCycle : BaseModel
    {
        public float CurrentTime { get; private set; }
        public float DayLength { get; private set; }
        
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
        
        private TimeOfDay GetTimeOfDay()
        {
            float normalized = CurrentTime / DayLength;
            if (normalized < 0.25f)
                return TimeOfDay.Night;
            if (normalized < 0.5f)
                return TimeOfDay.Morning;
            if (normalized < 0.75f)
                return TimeOfDay.Day;
            return TimeOfDay.Evening;
        }
    }
}