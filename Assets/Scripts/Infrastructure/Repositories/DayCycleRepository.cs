using Application.EventSystem;
using Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Repositories
{
    public class DayCycleRepository : Repository, IDayCycleRepository
    {
        [Inject]
        public DayCycleRepository(IEventBus eventBus) : base(eventBus) {
        }

        public DayCycle Load()
        {
            DayCycleState state = new DayCycleState
            {   
                Time = 500f,
                DayLength = 1200f,
                TimeOfDay = TimeOfDay.Morning
            };
            DayCycle dayCycle = new DayCycle(state);
            SubscribeOnModel(dayCycle);
            return dayCycle;
        }

        public void Save(DayCycle dayCycle)
        {
            throw new System.NotImplementedException();
        }
    }
}