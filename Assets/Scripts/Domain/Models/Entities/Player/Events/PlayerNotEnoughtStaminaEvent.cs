namespace Domain.Models.Entities.Player.Events
{
    public class PlayerNotEnoughtStaminaEvent : PlayerEvent
    {
        public PlayerNotEnoughtStaminaEvent(object sender, PlayerState playerState) : base(sender, playerState)
        {
        }
    }
}