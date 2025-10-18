using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Handlers
{
    public class PlayerPassOutUpdater
    {
        private IPlayerSleepUsecase _playerSleepUsecase;
        private DayCycle _dayCycle;
        private Player _player;
        private int _playerPassOutHourDown = 6;
        private int _playerPassOutHourUp = 23;

        [Inject]
        public PlayerPassOutUpdater(
            IDayCycleRepository dayCycleRepository, 
            IPlayerRepository playerRepository, 
            IPlayerSleepUsecase playerSleepUsecase)
        {
            _playerSleepUsecase = playerSleepUsecase;
            _dayCycle = dayCycleRepository.Get();
            _player = playerRepository.Get();
            Subscribe();
        }
        
        private void Subscribe()
        {
            _dayCycle.OnNewHour += HandleNewHour;
        }

        private void Unsubscribe()
        {
            // TODO How to unsubscribe this
            _dayCycle.OnNewHour -= HandleNewHour;
        }

        private void HandleNewHour(NewHourEvent e)
        {
            if (e.Hour >= _playerPassOutHourUp || e.Hour < _playerPassOutHourDown) {
                _playerSleepUsecase.StartPassOut(_player);
            }
        }
    }
}