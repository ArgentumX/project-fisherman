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
                Time = 30,
                DayLength = 60,
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