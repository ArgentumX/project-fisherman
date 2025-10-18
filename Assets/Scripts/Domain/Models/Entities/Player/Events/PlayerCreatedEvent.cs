using Domain.Models.Entities.Player.Events;

namespace Domain.Models.Entities.Player
{
    public class PlayerCreatedEvent : PlayerEvent
    {
        public PlayerCreatedEvent(object sender, PlayerDto playerDto) : base(sender, playerDto)
        {
        }
    }
}