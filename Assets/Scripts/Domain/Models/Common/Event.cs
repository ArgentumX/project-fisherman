namespace Domain.Models.Common
{
    public abstract class Event
    {
        public object Sender { get; private set; }
        public Event(object sender)
        {
            Sender = sender;
        }
    }
}