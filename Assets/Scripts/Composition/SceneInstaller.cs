using Application.Interfaces;
using Application.Interfaces.EventProviders;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Handlers;
using Infrastructure.Repositories;
using Infrastructure.Usecases;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Factories
            Container.Bind<IDayCycleFactory>().To<DayCycleFactory>().AsTransient();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsTransient();
            Container.Bind<IQuestFactory>().To<QuestFactory>().AsTransient();
            
            // Repositories
            Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<IDayCycleRepository>().To<DayCycleRepository>().AsSingle().NonLazy();
            Container.Bind<IQuestRepository>().To<QuestRepository>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<RepositoriesInitializer>().AsSingle().NonLazy();
            
            // GameContext
            Container.Bind<IGameContext>().To<GameContext>().AsSingle().NonLazy();
            
            // Usecases
            Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsTransient();
            Container.Bind<IDayCycleUsecase>().To<DayCycleUsecase>().AsTransient();
            Container.Bind<IPlayerSleepUsecase>().To<PlayerSleepUsecase>().AsTransient();
            Container.Bind<IPlayerQuestsUsecase>().To<PlayerQuestsUsecase>().AsTransient();
            
            // Event Providers
            Container.Bind<ITickProvider>().To<TickProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            // Handlers
            Container.BindInterfacesAndSelfTo<DayCycleTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerPassOutTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<QuestsTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<QuestsGiver>().AsSingle().NonLazy();
        }
    }
}