namespace Domain.Models.Common.Events
{
    public class NewCycleEvent : Event
    {
        public int DaysPerCycle { get; }
        public int TotalDaysPassed { get; }
        public int CyclesPassed { get; }
        
        public NewCycleEvent(object sender, int dayPerCycle, int cyclePassed) : base(sender) {
            DaysPerCycle = dayPerCycle;
            CyclesPassed = cyclePassed;
            TotalDaysPassed = cyclePassed * DaysPerCycle;
        }
    }
}