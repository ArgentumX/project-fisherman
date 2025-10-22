using System;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestCompletedEvent : QuestEvent
    {
        public QuestCompletedEvent(object sender, Guid questId) : base(sender, questId)
        {
        }
    }
}