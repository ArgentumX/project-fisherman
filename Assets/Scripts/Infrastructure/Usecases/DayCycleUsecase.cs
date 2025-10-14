using Application.EventSystem;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Usecases
{
    public class DayCycleUsecase : Usecase, IDayCycleUsecase
    {
        private DayCycle _dayCycle;
        private IEventBus _eventBus;
        private IDayCycleRepository _dayCycleRepository;

        [Inject]
        public DayCycleUsecase(IEventBus eventBus, IDayCycleRepository dayCycleRepository)
        {
            _eventBus = eventBus;
            _dayCycleRepository = dayCycleRepository;
            _dayCycle = _dayCycleRepository.LoadDayCycle();
            
            
            _eventBus.Subscribe<LogicTickEvent>(OnLogicTick);
        }
        
        public void UpdateTime(float deltaTime)
        {
            _dayCycle.UpdateTime(this, deltaTime);    
        }

        public void SetTime(TimeOfDay timeOfDay)
        {
            throw new System.NotImplementedException();
        }

        private void OnLogicTick(LogicTickEvent logicTickEvent)
        {
            UpdateTime(logicTickEvent.DeltaTime);
        }
    }
}