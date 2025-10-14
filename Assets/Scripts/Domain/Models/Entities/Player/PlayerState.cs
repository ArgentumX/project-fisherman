namespace Domain.Models.Entities.Player
{
    public record PlayerState
    {
        public int Health { get; set; }
        public float Stamina { get; set; }
        public float MaxStamina { get; set; }
    }
}