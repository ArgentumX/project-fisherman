using Domain.Models.Common;

namespace Application.EventSystem
{
    public interface IDomainEventsPublisher
    {
        void Register(BaseModel model);
        void Unregister(BaseModel model);
    }
}