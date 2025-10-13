using System;
using System.Collections.Generic;
using Domain.Events.Common;

namespace Application.Interfaces.EventSystem
{
    public interface IEventBus
    {
        void Publish<T>(T domainEvent) where T : BaseEvent;
        void Publish<T>(IReadOnlyCollection<T> domainEvents) where T : BaseEvent;
        void Subscribe<T>(Action<T> handler) where T : BaseEvent;
        void Unsubscribe<T>(Action<T> handler) where T : BaseEvent;
    }
}