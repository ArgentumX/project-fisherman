using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using Zenject;

namespace Infrastructure.Handlers
{
    public class QuestsGiver
    {
        private IPlayerQuestsUsecase _playerQuestsUsecase;
        private DayCycle _dayCycle;
        // TODO replace day passed info to GameModel or to some special Service
        private int _dayPassed = 0;
        private int _giveQuestsInterval = 1;
        
        [Inject]
        private QuestsGiver(IPlayerQuestsUsecase playerQuestsUsecase, IDayCycleRepository dayCycleRepository)
        {
            _playerQuestsUsecase = playerQuestsUsecase;
            _dayCycle = dayCycleRepository.GetInstance();
            SubscribeToDayCycle(_dayCycle);
        }
        private void SubscribeToDayCycle(DayCycle dayCycle)
        {
            dayCycle.OnNewDay += HandleNewDayEvent;
        }
        private void UnsubscribeFromDayCycle(DayCycle dayCycle)
        {
            dayCycle.OnNewDay -= HandleNewDayEvent;
        }
        private void HandleNewDayEvent(NewDayEvent newDayEvent)
        {
            _dayPassed++;
            if (_dayPassed % _giveQuestsInterval == 0)
                GiveNewQuestsPack();
        }

        private void GiveNewQuestsPack() {
            _playerQuestsUsecase.GiveNewQuestPack();
        }
    }
}