using System;
using Application.Interfaces.Repositories;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using Infrastructure.Settings;
using Zenject;

namespace Infrastructure.Handlers
{
    public class CycleProvider 
    {
        public event Action<NewCycleEvent> OnNewCycle;
        
        private readonly GameSettings _gameSettings;
        private int cyclesPassed = 0;
        private int daysFromLastCycle = 0;
        private DayCycle _dayCycle;
        
        [Inject]
        private CycleProvider(
            IDayCycleRepository dayCycleRepository,
            GameSettings gameSettings
        ) {
            _gameSettings = gameSettings;
            _dayCycle = dayCycleRepository.GetInstance();
            Subscribe(_dayCycle);
        }

        private void Subscribe(DayCycle dayCycle)
        {
            _dayCycle.OnNewDay += HandleNewDayEvent;
        }

        private void Unsubscribe(DayCycle dayCycle) {
            _dayCycle.OnNewDay -= HandleNewDayEvent;
        }

        private void HandleNewDayEvent(NewDayEvent dayEvent) {
            daysFromLastCycle++;
            if (daysFromLastCycle >= _gameSettings.DaysPerCycle) {
                daysFromLastCycle = 0;
                cyclesPassed++;
                var e = new NewCycleEvent(this, _gameSettings.DaysPerCycle, cyclesPassed);
                OnNewCycle?.Invoke(e);
            }
        }
    }
}