namespace Domain.Models.Entities.Player.Events
{
    public class PlayerNotEnoughStaminaEvent : PlayerEvent
    {
        public PlayerNotEnoughStaminaEvent(object sender, PlayerDto playerDto) : base(sender, playerDto)
        {
        }
    }
}