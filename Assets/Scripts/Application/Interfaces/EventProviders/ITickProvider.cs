using System;
using Domain.Models.Common.Events;

namespace Application.Interfaces.EventProviders
{
    public interface ITickProvider
    {
        public event Action<LogicTickEvent> OnLogicTick; 
        public event Action<ViewTickEvent> OnViewTick; 
        void ProvideLogicTick(float deltaTime);
        void ProvideViewTick(float deltaTime);
    }
}