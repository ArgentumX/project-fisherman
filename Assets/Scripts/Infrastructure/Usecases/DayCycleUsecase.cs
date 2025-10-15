using Application.EventSystem;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Common.Events;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Usecases
{
    public class DayCycleUsecase : IDayCycleUsecase
    {
        private IEventBus _eventBus;
        private IDayCycleRepository _dayCycleRepository;
        private IDayCycleFactory _dayCycleFactory;

        [Inject]
        public DayCycleUsecase(IEventBus eventBus, IDayCycleRepository dayCycleRepository, IDayCycleFactory dayCycleFactory)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<LogicTickEvent>(OnLogicTick);
            _dayCycleRepository = dayCycleRepository;
            _dayCycleFactory  = dayCycleFactory;
            CreateDayCycle();
        }
        
        
        public void UpdateTime(float deltaTime)
        {
            _dayCycleRepository.Get().UpdateTime(this, deltaTime);    
        }

        public void SetTime(TimeOfDay timeOfDay)
        {
            throw new System.NotImplementedException();
        }

        private void OnLogicTick(LogicTickEvent logicTickEvent)
        {
            UpdateTime(logicTickEvent.DeltaTime);
        }

        private void CreateDayCycle()
        {
            var dayCycle = _dayCycleFactory.CreateDefault();
            _dayCycleRepository.Save(dayCycle);
        }
    }
}