using System;
using System.Collections.Generic;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Quest;
using Zenject;

namespace Infrastructure.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private readonly Dictionary<Guid, Quest> _quests = new();

        [Inject]
        public QuestRepository(IQuestFactory factory)
        {
            foreach (var quest in factory.CreateDefault()){
                Save(quest);
            }
        }
        
        public Quest Get(Guid id)
        {
            if (_quests.TryGetValue(id, out var quest))
                return quest;
            
            throw new KeyNotFoundException($"Quest with ID {id} was not found.");
        }

        public IEnumerable<Quest> GetAll()
        {
            return _quests.Values;
        }
        
        // TODO replace Save => Add, Remove and Save only for data work
        public void Save(Quest target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            
            _quests[target.Id] = target;
        }
    }
}