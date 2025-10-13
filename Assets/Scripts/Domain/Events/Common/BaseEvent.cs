namespace Domain.Events.Common
{
    public abstract class BaseEvent
    {
        public object Sender { get; }

        protected BaseEvent(object sender)
        {
            Sender = sender;
        }
    }
}