using Application.EventSystem;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Factories
{
    // In child classes always call CreateInternal. CreateInternal calls CreateModel, which implements in child classes 
    public abstract class DomainEventsPublishFactory
    {
        protected readonly IDomainEventsPublisher _domainEventsPublisher;
        
        [Inject]
        protected DomainEventsPublishFactory(IDomainEventsPublisher domainEventsPublisher)
        {
            _domainEventsPublisher = domainEventsPublisher;
        }

        protected void RegisterModel(BaseModel model)
        {
            _domainEventsPublisher.Register(model);
        }
        
        protected void UnregisterModel(BaseModel model)
        {
            _domainEventsPublisher.Unregister(model);
        }
    }
}