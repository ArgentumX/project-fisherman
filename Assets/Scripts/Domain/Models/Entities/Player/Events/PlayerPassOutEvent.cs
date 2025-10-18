namespace Domain.Models.Entities.Player.Events
{
    public class PlayerPassOutEvent : PlayerEvent
    {
        public bool IsCompleted { get; private set; }
        public PlayerPassOutEvent(object sender, PlayerDto playerDto, bool isCompleted) : base(sender, playerDto)
        {
            IsCompleted = isCompleted;
        }
    }
}