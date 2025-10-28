using System.Collections.Generic;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Quest;

namespace Application.Interfaces.Factories
{
    public interface IQuestFactory
    {
        List<Quest> CreateDefault(IGameContext context);
    }
}