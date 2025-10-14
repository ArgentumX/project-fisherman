using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerTakeDamageEvent : PlayerEvent
    {
        public PlayerTakeDamageEvent(object sender, PlayerState playerState) : base(sender, playerState)
        {
        }
    }
}