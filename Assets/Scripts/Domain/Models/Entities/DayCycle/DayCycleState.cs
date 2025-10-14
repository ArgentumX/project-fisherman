using Domain.Enums;

namespace Domain.Models.Entities.DayCycle
{
    public record DayCycleState
    {
        public float Time { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public float DayLength { get; set; }
    }
}