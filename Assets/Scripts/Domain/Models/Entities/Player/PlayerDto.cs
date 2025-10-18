using System.Numerics;

namespace Domain.Models.Entities.Player
{
    public struct PlayerDto
    {
        public int Health { get; set; }
        public float Stamina { get; set; }
        public float MaxStamina { get; set; }
        public Vector3 Position { get; set; }
    }
}