using Application.EventSystem;
using Application.Interfaces.EventSystem;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IPlayerRepository>().To<PlayerRepository>().AsSingle();
            // Container.Bind<IPlayerUsecase>().To<PlayerUsecase>().AsSingle();
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }
    }
}