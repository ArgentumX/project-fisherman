using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerStaminaChangedEvent : PlayerEvent
    {
        public PlayerStaminaChangedEvent(object sender, PlayerState playerState) : base(sender, playerState)
        {
        }
    }
}