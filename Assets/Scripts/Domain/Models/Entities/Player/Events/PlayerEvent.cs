using Domain.Models.Common;

namespace Domain.Models.Entities.Player.Events
{
    public class PlayerEvent : BaseEvent
    {
        public PlayerDto PlayerDto { get; private set; }
        
        public PlayerEvent(object sender, PlayerDto playerDto) : base(sender)
        {
            PlayerDto = playerDto;
        }
    }
}