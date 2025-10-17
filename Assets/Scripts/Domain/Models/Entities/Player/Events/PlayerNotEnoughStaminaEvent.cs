namespace Domain.Models.Entities.Player.Events
{
    public class PlayerNotEnoughStaminaEvent : PlayerEvent
    {
        public PlayerNotEnoughStaminaEvent(object sender, PlayerState playerState) : base(sender, playerState)
        {
        }
    }
}