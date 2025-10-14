namespace Domain.Models.Entities.DayCycle.Events
{
    public class DayCycleChangedEvent : DayCycleEvent
    {
        public DayCycleChangedEvent(object sender, DayCycleState state) : base(sender, state)
        {
        }
    }
}