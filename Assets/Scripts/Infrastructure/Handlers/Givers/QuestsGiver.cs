using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using Zenject;

namespace Infrastructure.Handlers
{
    public class QuestsGiver
    {
        private IPlayerQuestsUsecase _playerQuestsUsecase;
        private CycleProvider _cycleProvider;
        
        [Inject]
        private QuestsGiver(IPlayerQuestsUsecase playerQuestsUsecase, CycleProvider cycleProvider)
        {
            _playerQuestsUsecase = playerQuestsUsecase;
            _cycleProvider = cycleProvider;
            Subscribe(cycleProvider);
        }
        private void Subscribe(CycleProvider cycleProvider) {
            cycleProvider.OnNewCycle += HandleNewCycle;
        }
        private void Unsubscribe(CycleProvider cycleProvider)
        {
            cycleProvider.OnNewCycle -= HandleNewCycle;
        }
        private void HandleNewCycle(NewCycleEvent newCycleEvent) {
            _playerQuestsUsecase.GiveNewQuestPack();
        }
    }
}