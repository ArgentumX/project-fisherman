using Application.Interfaces.EventSystem;
using Domain.Models.Common;
using Zenject;

namespace Infrastructure.Repositories
{
    public abstract class Repository
    {
        
        private IEventBus _eventBus;

        [Inject]
        protected Repository(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // Subscribe all new models
        private void SubscribeModel(BaseModel model)
        {
            model.OnAddDomainEvent += HandleAddDomainEvent;
        }

        private void UnsubscribeModel(BaseModel model)
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