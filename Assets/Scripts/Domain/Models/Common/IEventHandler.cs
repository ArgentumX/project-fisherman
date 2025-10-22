namespace Domain.Models.Common
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(TEvent e);
    }
}