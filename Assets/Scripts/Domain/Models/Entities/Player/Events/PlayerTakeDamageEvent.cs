using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerTakeDamageEvent : BaseEvent
    {
        public PlayerState State { get; private set; }
        public PlayerTakeDamageEvent(object sender, PlayerState state) : base(sender)
        {
            State = state;
        }
    }
}