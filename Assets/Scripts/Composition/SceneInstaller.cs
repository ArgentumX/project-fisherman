using Application.EventSystem;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Infrastructure.Repositories;
using Infrastructure.Usecases;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsTransient();
            Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsTransient();
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }
    }
}