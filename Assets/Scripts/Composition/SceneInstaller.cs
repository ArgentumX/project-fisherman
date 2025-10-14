using Application.EventSystem;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Usecases;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
            Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsTransient();
            Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsTransient();
            Container.Bind<ITickProvider>().To<TickProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}