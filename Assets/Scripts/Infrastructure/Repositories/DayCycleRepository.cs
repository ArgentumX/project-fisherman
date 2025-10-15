using Application.EventSystem;
using Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Repositories
{
    public class DayCycleRepository : IDayCycleRepository
    {
        private DayCycle _dayCycle;
        
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