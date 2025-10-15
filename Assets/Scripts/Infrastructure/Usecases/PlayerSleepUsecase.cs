using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerSleepUsecase : ISleepUsecase
    {
        private IDayCycleRepository _dayCycleRepository;
        private IPlayerRepository _playerRepository;

        [Inject]
        public PlayerSleepUsecase(IDayCycleRepository dayCycleRepository, IPlayerRepository playerRepository)
        {
            _dayCycleRepository = dayCycleRepository;
            _playerRepository = playerRepository;
        }
        
        public bool TrySleep()
        {
            if (!IsPossibleToSleep())
                return false;
            
            _dayCycleRepository.Get().SetTimeOfDay(TimeOfDay.Morning);
            return true;
        }

        public bool IsPossibleToSleep()
        {
            var timeOfDay = _dayCycleRepository.Get().TimeOfDay;
            if (timeOfDay is TimeOfDay.Evening or TimeOfDay.Night) {
                return true;
            }

            return false;
        }
    }
}