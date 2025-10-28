using System;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Repositories
{
    public class DayCycleRepository : IDayCycleRepository
    {
        private DayCycle _dayCycle;
        public DayCycleRepository() { }
        
        public DayCycle GetInstance()
        {
            return _dayCycle;
        }
        
        public void Save(DayCycle dayCycle)
        {
            _dayCycle = dayCycle;
        }
    }
}