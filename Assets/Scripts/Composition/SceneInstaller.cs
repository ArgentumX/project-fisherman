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
            
            // Repositories
            Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<IDayCycleRepository>().To<DayCycleRepository>().AsSingle().NonLazy();
            
            // Usecases
            Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsTransient();
            Container.Bind<IDayCycleUsecase>().To<DayCycleUsecase>().AsTransient();
            Container.Bind<IPlayerSleepUsecase>().To<PlayerSleepUsecase>().AsTransient();
            
            // Event Providers
            Container.Bind<ITickProvider>().To<TickProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            // Handlers
            Container.Bind<DayCycleUpdater>().AsSingle().NonLazy();
            Container.Bind<PlayerPassOutUpdater>().AsSingle().NonLazy();
        }
    }
}