using Application.EventSystem;
using Domain.Models.Common;
using Zenject;

namespace Infrastructure.EventSystem
{
    public class DomainEventsPublisher : IDomainEventsPublisher
    {
        private IEventBus _eventBus;
        [Inject]
        public DomainEventsPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Register(BaseModel model)
        {
            model.OnAddDomainEvent += HandleAddDomainEvent;
        }

        public void Unregister(BaseModel model)
        {
            model.OnAddDomainEvent -= HandleAddDomainEvent;
        }

        private void HandleAddDomainEvent(BaseModel model)
        {
            _eventBus.Publish(model.DomainEvents);
            model.ClearDomainEvents();
        }
    }
}