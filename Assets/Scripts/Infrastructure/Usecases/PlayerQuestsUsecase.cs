using System.Linq;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Quest;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerQuestsUsecase : IPlayerQuestsUsecase
    {
        private IQuestRepository _questRepository;
        [Inject]
        private PlayerQuestsUsecase(IQuestRepository questRepository)
        {
            _questRepository = questRepository;
        }
        public void GiveNewQuestPack()
        {
            var notStartedQuests = _questRepository
                .GetAll()
                .Where(q => q.Status == QuestStatus.NotStarted)
                .Take(3)
                .ToList();
            
            foreach (var quest in notStartedQuests) {
                quest.StartQuest();
            }
        }
    }
}