

using Application.Interfaces.EventProviders;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Handlers
{
    public class DayCycleUpdater
    {
        private ITickProvider _tickProvider;
        private IDayCycleUsecase _dayCycleUsecase;
        private DayCycle _dayCycle;

        [Inject]
        public DayCycleUpdater(ITickProvider tickProvider, IDayCycleUsecase dayCycleUsecase, IDayCycleRepository dayCycleRepository)
        {
            _tickProvider = tickProvider;
            _dayCycleUsecase = dayCycleUsecase;
            _dayCycle = dayCycleRepository.Get();
            Subscribe();
        }

        private void Subscribe()
        {
            _tickProvider.OnViewTick += HandleTick;
        }

        private void Unsubscribe()
        {
            // TODO How to unsubscribe this
            _tickProvider.OnViewTick -= HandleTick;
        }
        
        private void HandleTick(ViewTickEvent viewTickEvent)
        {
            _dayCycleUsecase.UpdateTime(_dayCycle, viewTickEvent.DeltaTime);
        }
        
    }
}