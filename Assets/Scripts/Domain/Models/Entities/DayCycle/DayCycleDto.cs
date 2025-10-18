using Domain.Enums;

namespace Domain.Models.Entities.DayCycle
{
    public record DayCycleDto
    {
        public float Time { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public float DayLength { get; set; }
    }
}