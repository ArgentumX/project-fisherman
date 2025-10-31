using Domain.Models.Common;

namespace Domain.Models.Entities.DayCycle.Events
{
    public class DayCycleEvent : Event
    {
        public DayCycleDto DayCycleDto { get; private set; }
        
        public DayCycleEvent(object sender, DayCycleDto dto) : base(sender)
        {
            DayCycleDto = dto;
        }
    }
}