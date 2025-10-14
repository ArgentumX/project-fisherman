using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerEvent : BaseEvent
    {
        public PlayerState PlayerState { get; private set; }
        
        public PlayerEvent(object sender, PlayerState playerState) : base(sender)
        {
            PlayerState = playerState;
        }
    }
}