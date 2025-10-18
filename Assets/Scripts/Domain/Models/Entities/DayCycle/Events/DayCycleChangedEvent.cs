namespace Domain.Models.Entities.DayCycle.Events
{
    public class DayCycleChangedEvent : DayCycleEvent
    {
        public DayCycleChangedEvent(object sender, DayCycleDto dto) : base(sender, dto)
        {
        }
    }
}