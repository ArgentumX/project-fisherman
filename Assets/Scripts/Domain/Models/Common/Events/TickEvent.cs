namespace Domain.Models.Common.Events
{
    public abstract class TickEvent : Event
    {
        public float DeltaTime { get; private set; }
        protected TickEvent(object sender, float deltaTime) : base(sender)
        {
            DeltaTime = deltaTime;
        }
    }
}