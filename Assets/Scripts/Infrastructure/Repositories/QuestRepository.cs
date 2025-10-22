using System;
using System.Collections;
using System.Collections.Generic;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Quest;

namespace Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private readonly Dictionary<Guid, Quest> _quests = new();

        public QuestRepository()
        {
            // TODO factory
            var q1 = new StaminaQuest("TEST GET MAX STAMIN", QuestStatus.Active);
            _quests.Add(q1.Id, q1);
        }
        
        public Quest Get(Guid id)
        {
            if (_quests.TryGetValue(id, out var quest))
                return quest;
            
            throw new KeyNotFoundException($"Quest with ID {id} was not found.");
        }

        public IEnumerable GetAll()
        {
            return _quests.Values;
        }

        public void Save(Quest target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            
            _quests[target.Id] = target;
        }
    }
}