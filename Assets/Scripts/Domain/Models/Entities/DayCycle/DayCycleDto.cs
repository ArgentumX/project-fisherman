using Domain.Enums;

namespace Domain.Models.Entities.DayCycle
{
    public struct DayCycleDto
    {
        public float Time { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public float DayLength { get; set; }
    }
}