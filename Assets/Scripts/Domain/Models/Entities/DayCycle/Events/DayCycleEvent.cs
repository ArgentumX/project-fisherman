using Domain.Models.Common;

namespace Domain.Models.Entities.DayCycle.Events
{
    public class DayCycleEvent : BaseEvent
    {
        public DayCycleState DayCycleState { get; private set; }
        
        public DayCycleEvent(object sender, DayCycleState state) : base(sender)
        {
            DayCycleState = state;
        }
    }
}