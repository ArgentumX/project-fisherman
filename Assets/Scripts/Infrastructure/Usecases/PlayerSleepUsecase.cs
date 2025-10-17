using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerSleepUsecase : IPlayerSleepUsecase
    {
        private IDayCycleRepository _dayCycleRepository;
        private IPlayerRepository _playerRepository;

        [Inject]
        public PlayerSleepUsecase(IDayCycleRepository dayCycleRepository, IPlayerRepository playerRepository)
        {
            _dayCycleRepository = dayCycleRepository;
            _playerRepository = playerRepository;
        }
        
        public bool TrySleep(Player player)
        {
            if (!IsPossibleToSleep(player))
                return false;
            
            player.RestoreStamina(this, player.MaxStamina);
            _dayCycleRepository.Get().SetTimeOfDay(TimeOfDay.Morning);
            return true;
        }

        public bool IsPossibleToSleep(Player player)
        {
            var timeOfDay = _dayCycleRepository.Get().TimeOfDay;
            if (timeOfDay is TimeOfDay.Evening or TimeOfDay.Night) {
                return true;
            }

            return false;
        }
    }
}