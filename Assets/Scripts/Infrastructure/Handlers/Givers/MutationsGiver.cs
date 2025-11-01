using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;

namespace Infrastructure.Handlers
{
    public class MutationsGiver
    {
        private IPlayerRepository _playerRepository;
        private IMutationsUsecase _mutationUsecase;
        private CycleProvider _cycleProvider;
        
        private MutationsGiver(
            IPlayerRepository playerRepository,
            IMutationsUsecase mutationsUsecase, 
            CycleProvider cycleProvider
        )
        {
            _playerRepository = playerRepository;
            _cycleProvider = cycleProvider;
            _mutationUsecase =  mutationsUsecase;
            Subscribe(_cycleProvider);
        }
        private void Subscribe(CycleProvider cycleProvider) {
            cycleProvider.OnNewCycle += HandleNewCycle;
        }
        private void Unsubscribe(CycleProvider cycleProvider)
        {
            cycleProvider.OnNewCycle -= HandleNewCycle;
        }
        private void HandleNewCycle(NewCycleEvent newCycleEvent) {
            var player = _playerRepository.GetInstance();
            _mutationUsecase.AddRandomMutation(player);
        }
    }
}