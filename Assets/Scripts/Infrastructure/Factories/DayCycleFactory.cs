using Application.EventSystem;
using Application.Interfaces.Factories;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;

namespace Infrastructure.Factories
{
    public class DayCycleFactory : DomainEventsPublishFactory, IDayCycleFactory
    {
        public DayCycleFactory(IDomainEventsPublisher domainEventsPublisher) : base(domainEventsPublisher)
        {
        }

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
            RegisterModel(dayCycle);
            return dayCycle;
        }
        
    }
}