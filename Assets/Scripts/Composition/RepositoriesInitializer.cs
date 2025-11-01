using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Infrastructure.Settings;
using Zenject;

namespace Composition
{
    public class RepositoriesInitializer
    {
        [Inject]
        public RepositoriesInitializer(
            IPlayerRepository playerRepo,
            IDayCycleRepository dayCycleRepo,
            IQuestRepository questRepo,
            IPlayerFactory playerFactory,
            IDayCycleFactory dayCycleFactory,
            IQuestFactory questFactory,
            IGameContext context)
        {
            // Player
            var player = playerFactory.CreateDefault();
            playerRepo.Save(player);

            // DayCycle
            var dayCycle = dayCycleFactory.CreateDefault();
            dayCycleRepo.Save(dayCycle);

            // Quests
            foreach (var quest in questFactory.CreateDefault(context))
            {
                questRepo.Save(quest);
            }
        }
        
    }
}