using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;

namespace Infrastructure.Handlers
{
    public class MutationsGiver
    {
        private DayCycle _dayCycle;
        private IPlayerRepository _playerRepository;
        private IMutationsUsecase _mutationUsecase;
        private int _dayPassed = 0;
        private int _giveMutationsInterval = 1;
        // TODO create cycle service with event OnNewCycle
        
        private MutationsGiver(
            IDayCycleRepository dayCycleRepository, 
            IMutationsUsecase mutationsUsecase,
            IPlayerRepository playerRepository
        )
        {
            _dayCycle = dayCycleRepository.GetInstance();
            _mutationUsecase = mutationsUsecase;
            _playerRepository = playerRepository;
            SubscribeToDayCycle(_dayCycle);
        }
        private void SubscribeToDayCycle(DayCycle dayCycle) {
            dayCycle.OnNewDay += HandleNewDayEvent;
        }
        private void UnsubscribeFromDayCycle(DayCycle dayCycle) {
            dayCycle.OnNewDay -= HandleNewDayEvent;
        }
        private void HandleNewDayEvent(NewDayEvent newDayEvent) {
            _dayPassed++;
            if (_dayPassed % _giveMutationsInterval == 0)
                GivePlayerNewMutation();
        }
        
        private void GivePlayerNewMutation() {
            var player = _playerRepository.GetInstance();
            _mutationUsecase.AddRandomMutation(player);
        }
    }
}