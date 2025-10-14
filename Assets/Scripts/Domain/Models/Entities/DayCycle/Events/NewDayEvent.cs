namespace Domain.Models.Entities.DayCycle.Events
{
    public class NewDayEvent : DayCycleEvent
    {
        public NewDayEvent(object sender, DayCycleState state) : base(sender, state)
        {
        }
    }
}