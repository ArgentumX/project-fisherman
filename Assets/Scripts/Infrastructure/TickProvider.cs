using Application.EventSystem;
using Application.Interfaces;
using Domain.Models.Common.Events;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class TickProvider : MonoBehaviour, ITickProvider
    {
        private IEventBus _eventBus;
        private const float LogicInterval = 0.1f;
        private float _logicTimer = 0;
        public TickProvider(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        [Inject]  
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void ProvideLogicTick(float deltaTime)
        { 
            var tick = new LogicTickEvent(this, deltaTime);
            _eventBus.Publish(tick);
        }

        public void ProvideViewTick(float deltaTime)
        {
            var tick = new ViewTickEvent(this, deltaTime);
            _eventBus.Publish(tick);
        }

        private void Update()
        {
            // TODO if (_paused) return
            ProvideViewTick(Time.deltaTime);
            _logicTimer += Time.deltaTime;
            if (_logicTimer >= LogicInterval)
            {
                ProvideLogicTick(_logicTimer);
                _logicTimer = 0;
            }
        }
    }
}