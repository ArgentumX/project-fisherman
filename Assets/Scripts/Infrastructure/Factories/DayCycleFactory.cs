using Application.Interfaces.Factories;
using Domain.Models.Entities.DayCycle;

namespace Infrastructure.Factories
{
    public class DayCycleFactory : IDayCycleFactory
    {
        private DayCycleFactory() { }
        public DayCycle CreateDefault()
        {
            var state = new DayCycleState
            {
                Time = 60,
                DayLength = 120,
            };
            return Create(state);
        }

        public DayCycle Create(DayCycleState state)
        {
            var dayCycle = new DayCycle(state);
            return dayCycle;
        }
        
    }
}