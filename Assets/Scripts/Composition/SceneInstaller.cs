using Application.Interfaces;
using Application.Interfaces.EventProviders;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Handlers;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Infrastructure.Usecases;
using Presentation.PlayerPresentation.Controllers;
using Presentation.Services;
using UnityEngine;
using Yarn.Unity;
using Zenject;


namespace Composition
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private DialogueRunner dialogueRunner;
        // TODO evolved input system should fix this shit 
        [SerializeField] private MovementController movementController;
        [SerializeField] private InteractionController interactionController;
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();;
            Container.BindInstance(dialogueRunner).AsSingle();
            
            // Factories
            Container.Bind<IDayCycleFactory>().To<DayCycleFactory>().AsTransient();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsTransient();
            Container.Bind<IQuestFactory>().To<QuestFactory>().AsTransient();
            Container.Bind<IMutationFactory>().To<MutationFactory>().AsTransient();
            
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
            Container.Bind<IMutationsUsecase>().To<PlayerMutationsUsecase>().AsTransient();
            
            // Event Providers
            Container.BindInterfacesTo<TickProvider>().AsSingle().NonLazy();
            Container.Bind<CycleProvider>().AsSingle().NonLazy();
            
            // Givers
            Container.Bind<QuestsGiver>().AsSingle().NonLazy();
            Container.Bind<MutationsGiver>().AsSingle().NonLazy();
            
            // Trackers
            Container.Bind<DayCycleTracker>().AsSingle().NonLazy();
            Container.Bind<PlayerPassOutTracker>().AsSingle().NonLazy();
            Container.Bind<QuestsTracker>().AsSingle().NonLazy();
            
            // Presenter
            // * Controllers
            Container.BindInstance(movementController).AsSingle();
            Container.BindInstance(interactionController).AsSingle();
            // * Services
            Container.BindInterfacesAndSelfTo<DialogBlocker>().AsSingle().NonLazy();
        }
    }
}