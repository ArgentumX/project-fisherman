using System;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestUpdatedEvent : QuestEvent
    {
        public QuestUpdatedEvent(object sender, Guid questId) : base(sender, questId)
        {
        }
    }
}