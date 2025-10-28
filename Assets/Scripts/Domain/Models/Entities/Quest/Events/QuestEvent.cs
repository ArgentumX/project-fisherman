using System;
using Domain.Models.Common;

namespace Domain.Models.Entities.Quest.Events
{
    public class QuestEvent : BaseEvent
    {
        public Quest Quest { get; }
        public QuestEvent(object sender, Quest quest) : base(sender)
        {
            Quest = quest;
        }
    }
}