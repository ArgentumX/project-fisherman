using Application.Interfaces.EventSystem;
using Domain.Models.Common;

namespace Infrastructure.Repositories
{
    public abstract class Repository
    {
        
        private IEventBus _eventBus;
        
        protected Repository(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // Subscribe all new models
        protected void SubscribeModel(BaseModel model)
        {
            model.OnAddDomainEvent += HandleAddDomainEvent;
        }

        protected void UnsubscribeModel(BaseModel model)
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