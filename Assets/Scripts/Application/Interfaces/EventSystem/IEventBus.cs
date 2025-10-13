using System;
using System.Collections.Generic;
using Domain.Models.Common;

namespace Application.Interfaces.EventSystem
{
    public interface IEventBus
    {
        void Publish(BaseEvent domainEvent);
        void Publish(IEnumerable<BaseEvent> domainEvents);

        void Subscribe<T>(Action<T> handler) where T : BaseEvent;
        void Unsubscribe<T>(Action<T> handler) where T : BaseEvent;
    }
}