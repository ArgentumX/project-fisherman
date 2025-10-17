using System;
using Application.Interfaces.EventProviders;
using Domain.Models.Common.Events;
using UnityEngine;

namespace Infrastructure
{
    public class TickProvider : MonoBehaviour, ITickProvider
    {
        public event Action<LogicTickEvent> OnLogicTick;
        public event Action<ViewTickEvent> OnViewTick;
        
        private const float LogicInterval = 0.1f;
        private float _logicTimer = 0;
        
        
        public void ProvideLogicTick(float deltaTime)
        { 
            var tick = new LogicTickEvent(this, deltaTime);
            OnLogicTick?.Invoke(tick);
        }

        public void ProvideViewTick(float deltaTime)
        {
            var tick = new ViewTickEvent(this, deltaTime);
            OnViewTick?.Invoke(tick);
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