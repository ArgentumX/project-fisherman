using System;
using System.Collections.Generic;
using Application.EventSystem;
using Domain.Models.Common;

namespace Infrastructure.EventSystem
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();
        
        public void Publish(BaseEvent domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            var eventType = domainEvent.GetType();

            if (_handlers.TryGetValue(eventType, out var handlers))
            {
                var copy = new List<Delegate>(handlers);
                foreach (var handler in copy)
                {
                    handler.DynamicInvoke(domainEvent);
                }
            }
        }

        public void Publish(IEnumerable<BaseEvent> domainEvents)
        {
            foreach (var e in domainEvents)
                Publish(e);
        }

        public void Subscribe<T>(Action<T> handler) where T : BaseEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _handlers[type] = list;
            }

            if (!list.Contains(handler))
                list.Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : BaseEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var list))
            {
                list.Remove(handler);
                if (list.Count == 0)
                    _handlers.Remove(type);
            }
        }
    }
}