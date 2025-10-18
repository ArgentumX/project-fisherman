using Application.Interfaces.Factories;
using Domain.Models.Entities.DayCycle;

namespace Infrastructure.Factories
{
    public class DayCycleFactory : IDayCycleFactory
    {
        private DayCycleFactory() { }
        public DayCycle CreateDefault()
        {
            var state = new DayCycleDto
            {
                Time = 60,
                DayLength = 120,
            };
            return Create(state);
        }

        public DayCycle Create(DayCycleDto dto)
        {
            var dayCycle = new DayCycle(dto);
            return dayCycle;
        }
        
    }
}