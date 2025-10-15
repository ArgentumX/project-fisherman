namespace Domain.Models.Entities.Player.Events
{
    public class PlayerCreatedEvent : PlayerEvent
    {
        public PlayerCreatedEvent(object sender, PlayerState playerState) : base(sender, playerState)
        {
        }
    }
}