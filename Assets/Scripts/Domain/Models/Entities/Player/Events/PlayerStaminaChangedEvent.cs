using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerStaminaChangedEvent : PlayerEvent
    {
        public PlayerStaminaChangedEvent(object sender, PlayerDto playerDto) : base(sender, playerDto)
        {
        }
    }
}