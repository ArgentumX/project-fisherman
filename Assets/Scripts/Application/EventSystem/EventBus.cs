using System;
using System.Collections.Generic;
using Application.Interfaces.EventSystem;
using Domain.Events.Common;

namespace Application.EventSystem
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new ();

        public void Publish<T>(T domainEvent) where T : BaseEvent
        {
            if (domainEvent == null) {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            var eventType = typeof(T);
            if (_handlers.TryGetValue(eventType, out var handlers))
            {
                // Create a copy to avoid modification during iteration
                var handlersCopy = new List<Delegate>(handlers);
                foreach (var handler in handlersCopy)
                {
                    ((Action<T>)handler)(domainEvent);
                }
            }
        }

        public void Publish<T>(IReadOnlyCollection<T> domainEvents) where T : BaseEvent
        {
            foreach (var domainEvent in domainEvents)
            {
                Publish(domainEvent);
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : BaseEvent
        {
            if (handler == null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var eventType = typeof(T);
            if (!_handlers.TryGetValue(eventType, out var handlers))
            {
                handlers = new List<Delegate>();
                _handlers[eventType] = handlers;
            }

            if (!handlers.Contains(handler))
            {
                handlers.Add(handler);
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : BaseEvent
        {
            if (handler == null) {
                throw new ArgumentNullException(nameof(handler));
            }

            var eventType = typeof(T);
            if (_handlers.TryGetValue(eventType, out var handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _handlers.Remove(eventType);
                }
            }
        }
    }
}