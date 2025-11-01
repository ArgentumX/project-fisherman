using Application.Interfaces.Factories;
using Domain.Models.Entities.DayCycle;
using Infrastructure.Settings;
using Zenject;

namespace Infrastructure.Factories
{
    public class DayCycleFactory : IDayCycleFactory
    {
        private readonly GameSettings _gameSettings;
        
        [Inject]
        private DayCycleFactory(GameSettings gameSettings) {
            _gameSettings = gameSettings;
        }
        public DayCycle CreateDefault()
        {
            var state = new DayCycleDto
            {
                Time = _gameSettings.TimeStartInSec,
                DayLength = _gameSettings.DayLengthInSec,
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