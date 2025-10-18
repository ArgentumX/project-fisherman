using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerTakeDamageEvent : PlayerEvent
    {
        public PlayerTakeDamageEvent(object sender, PlayerDto playerDto) : base(sender, playerDto)
        {
        }
    }
}