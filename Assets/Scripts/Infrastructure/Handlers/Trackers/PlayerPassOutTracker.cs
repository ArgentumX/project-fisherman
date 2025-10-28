using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Handlers
{
    public class PlayerPassOutTracker
    {
        private IPlayerSleepUsecase _playerSleepUsecase;
        private DayCycle _dayCycle;
        private Player _player;
        private int _playerPassOutHourDown = 6;
        private int _playerPassOutHourUp = 23;

        [Inject]
        public PlayerPassOutTracker(
            IDayCycleRepository dayCycleRepository, 
            IPlayerRepository playerRepository, 
            IPlayerSleepUsecase playerSleepUsecase)
        {
            _playerSleepUsecase = playerSleepUsecase;
            _dayCycle = dayCycleRepository.GetInstance();
            _player = playerRepository.GetInstance();
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
                
                if (_player.IsSleep)
                    return;
                _playerSleepUsecase.StartPassOut(_player);
            }
        }
    }
}