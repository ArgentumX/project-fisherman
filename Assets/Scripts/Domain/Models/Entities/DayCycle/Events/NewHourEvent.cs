using Domain.Models.Common;

namespace Domain.Models.Entities.DayCycle.Events
{
    public class NewHourEvent : DayCycleEvent
    {
        public int Hour { get; }
        public NewHourEvent(object sender, DayCycleDto dto, int hour) : base(sender, dto)
        {
            Hour = hour;
        }
    }
}