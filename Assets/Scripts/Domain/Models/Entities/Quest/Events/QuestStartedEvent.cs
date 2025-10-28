using System;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestStartedEvent : QuestEvent
    {
        public QuestStartedEvent(object sender, Quest quest) : base(sender, quest)
        {
        }
    }
}