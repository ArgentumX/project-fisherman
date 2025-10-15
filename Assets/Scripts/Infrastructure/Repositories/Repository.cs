using Application.EventSystem;
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
        protected void SubscribeOnModel(BaseModel model)
        {
            model.OnAddDomainEvent += HandleAddDomainEvent;
        }

        protected void UnsubscribeFromModel(BaseModel model)
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