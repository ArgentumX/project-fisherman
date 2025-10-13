namespace Domain.Models.Common
{
    public abstract class BaseEvent
    {
        public object Sender { get; private set; }

        protected BaseEvent(object sender)
        {
            Sender = sender;
        }
    }
}