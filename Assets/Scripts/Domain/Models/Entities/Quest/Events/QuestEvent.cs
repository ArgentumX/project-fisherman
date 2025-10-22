using System;
using Domain.Models.Common;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestEvent : BaseEvent
    {
        public Guid QuestId { get; private set; }
        public QuestEvent(object sender, Guid questId) : base(sender)
        {
            QuestId = questId;
        }
    }
}