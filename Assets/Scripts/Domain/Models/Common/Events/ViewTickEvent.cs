namespace Domain.Models.Common.Events
{
    public class ViewTickEvent : TickEvent
    {
        public ViewTickEvent(object sender, float deltaTime) : base(sender, deltaTime)
        {
        }
    }
}