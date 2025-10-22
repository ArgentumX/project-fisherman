using System;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestFailedEvent : QuestEvent
    {
        public QuestFailedEvent(object sender, Guid questId) : base(sender, questId)
        {
        }
    }
}