using System;
using System.Collections.Generic;
using Domain.Models.Common;

namespace Domain.Models.Entities.Player
{
    public class PlayerQuestsProgress
    {
        public HashSet<Guid> ActiveQuestIds { get; } = new();
        public HashSet<Guid> CompletedQuestIds { get; } = new();
        public bool IsQuestActive(Guid questId) => ActiveQuestIds.Contains(questId);
        public bool IsQuestCompleted(Guid questId) => CompletedQuestIds.Contains(questId);
    }
}