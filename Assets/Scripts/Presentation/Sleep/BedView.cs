using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Infrastructure.Extensions;
using Presentation.Common;
using UnityEngine;
using Zenject;

namespace Presentation.Sleep
{
    [RequireComponent(typeof(Outline))]
    public class BedView : OutlineInteractable
    {
        [SerializeField] private Transform spawnTransform;
        private IPlayerSleepUsecase _playerPlayerSleepUsecase;
        
        [Inject]
        private void Construct(IPlayerSleepUsecase playerPlayerSleepUsecase, IPlayerRepository playerRepository)
        {
            // TODO wrong bed init
            _playerPlayerSleepUsecase = playerPlayerSleepUsecase;
            _playerPlayerSleepUsecase.SetPlayerBed(playerRepository.GetInstance(), spawnTransform.position.ToSystemVector3());
        }
        
        public override void Interact<T>(IInteractor<T> interactor)
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    _playerPlayerSleepUsecase.StartSleep(player);
                    // TODO await animation
                    _playerPlayerSleepUsecase.EndSleep(player);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public override bool CanInteract<T>(IInteractor<T> interactor)
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    return _playerPlayerSleepUsecase.IsPossibleToSleep(player);
                default:
                    throw new System.NotImplementedException();
            }
        }
        private void Reset()
        {
            ResetBase();
            spawnTransform ??= GetComponent<Transform>();
        }
    }
}