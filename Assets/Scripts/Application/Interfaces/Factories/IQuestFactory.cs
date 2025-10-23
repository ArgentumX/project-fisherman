using System.Collections.Generic;
using Domain.Models.Entities.Quest;

namespace Application.Interfaces.Factories
{
    public interface IQuestFactory
    {
        List<Quest> CreateDefault();
    }
}