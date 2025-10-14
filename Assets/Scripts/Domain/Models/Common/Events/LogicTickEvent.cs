namespace Domain.Models.Common.Events
{
    public class LogicTickEvent : TickEvent
    {
        public LogicTickEvent(object sender, float deltaTime) : base(sender, deltaTime)
        {
        }
    }
}