using System;
using System.Collections.Generic;
using Domain.Events.Common;

namespace Domain.Models.Common
{
    public abstract class BaseModel
    {
        private readonly List<BaseEvent> _domainEvents = new();
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
        public event Action<BaseModel> OnAddDomainEvent;
            
        protected void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
            OnAddDomainEvent?.Invoke(this);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
    }
}
