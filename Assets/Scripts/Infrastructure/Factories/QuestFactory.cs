using System.Collections.Generic;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Quest;
using Zenject;

namespace Infrastructure.Factories
{
    public class QuestFactory : IQuestFactory
    {
        public List<Quest> CreateDefault(PlayerDto initialData)
        {
            Quest q1 = new StaminaQuest("Get full stamina", initialData, QuestStatus.Active);
            Quest q2 = new SequentialCompositeQuest("Some quest", new List<Quest>() { new StaminaQuest("test sub", initialData) });
            
            var result = new List<Quest>();
            result.AddRange(new []
            {
                q1, q2
            });
            return result;
        }
    }
}