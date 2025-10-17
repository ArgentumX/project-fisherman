using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Repositories
{
    public class DayCycleRepository : IDayCycleRepository
    {
        private DayCycle _dayCycle;

        [Inject]
        public DayCycleRepository(IDayCycleFactory dayCycleFactory)
        {
            // TODO remove if file exists
            var dayCycle = dayCycleFactory.CreateDefault();
            Save(dayCycle);
        }
        
        public DayCycle Get()
        {
            return _dayCycle;
        }

        public void Save(DayCycle dayCycle)
        {
            _dayCycle = dayCycle;
        }
    }
}