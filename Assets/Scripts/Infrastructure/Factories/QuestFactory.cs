using System.Collections.Generic;
using Application.Interfaces.Factories;
using Domain.Models.Entities.Quest;

namespace Infrastructure.Factories
{
    public class QuestFactory : IQuestFactory
    {
        public List<Quest> CreateDefault()
        {
            Quest q1 = new StaminaQuest("Get full stamina", QuestStatus.Active);
            Quest q2 = new SequentialCompositeQuest("Some quest", new List<Quest>() { new StaminaQuest("test sub") });
            
            var result = new List<Quest>();
            result.AddRange(new []
            {
                q1, q2
            });
            return result;
        }
    }
}