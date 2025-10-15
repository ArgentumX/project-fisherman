using Application.EventSystem;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Infrastructure;
using Infrastructure.EventSystem;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Usecases;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle().NonLazy();
            Container.Bind<IDomainEventsPublisher>().To<DomainEventsPublisher>().AsSingle().NonLazy();
            
            // Factories
            Container.Bind<IDayCycleFactory>().To<DayCycleFactory>().AsTransient();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsTransient();
            
            // Repositories
            Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<IDayCycleRepository>().To<DayCycleRepository>().AsSingle().NonLazy();
            
            // Usecases
            Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsTransient().NonLazy();
            Container.Bind<IDayCycleUsecase>().To<DayCycleUsecase>().AsTransient().NonLazy();
            Container.Bind<ISleepUsecase>().To<PlayerSleepUsecase>().AsTransient().NonLazy();
            
            Container.Bind<ITickProvider>().To<TickProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}