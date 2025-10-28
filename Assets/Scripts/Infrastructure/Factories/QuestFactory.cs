using System.Collections.Generic;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Quest;
using Zenject;

namespace Infrastructure.Factories
{
    public class QuestFactory : IQuestFactory
    {
        public List<Quest> CreateDefault(IGameContext context)
        {
            Quest q1 = new StaminaQuest("Get full stamina", context);
            Quest q2 = new SequentialCompositeQuest("Some quest", context, new List<Quest>() { new StaminaQuest("test sub", context) });
            
            var result = new List<Quest>();
            result.AddRange(new []
            {
                q1, q2
            });
            return result;
        }
    }
}