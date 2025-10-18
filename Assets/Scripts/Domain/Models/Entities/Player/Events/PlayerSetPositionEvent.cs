namespace Domain.Models.Entities.Player.Events
{
    public class PlayerSetPositionEvent : PlayerEvent
    {
        public PlayerSetPositionEvent(object sender, PlayerDto playerDto) : base(sender, playerDto)
        {
        }
    }
}