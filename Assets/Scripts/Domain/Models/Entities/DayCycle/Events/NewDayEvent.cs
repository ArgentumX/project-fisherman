namespace Domain.Models.Entities.DayCycle.Events
{
    public class NewDayEvent : DayCycleEvent
    {
        public NewDayEvent(object sender, DayCycleDto dto) : base(sender, dto)
        {
        }
    }
}