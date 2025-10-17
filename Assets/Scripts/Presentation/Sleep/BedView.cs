using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Common;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;
using Presentation.Common;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Presentation.Sleep
{
    [RequireComponent(typeof(Outline))]
    public class BedView : MonoBehaviour, IInteractable
    {
        [SerializeField] private string description = "Спать";
        [SerializeField] private Outline outline;
        [SerializeField] private Color canInteractColor = Color.yellowGreen;
        [SerializeField] private Color cannotInteractColor = Color.coral;
        
        private IPlayerSleepUsecase _playerPlayerSleepUsecase;
            
        [Inject]
        private void Construct(IPlayerSleepUsecase playerPlayerSleepUsecase)
        {
            _playerPlayerSleepUsecase = playerPlayerSleepUsecase;
        }
        
        public void OnHoverEnter<T>(IInteractor<T> interactor) where T : BaseModel
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    bool canInteract = _playerPlayerSleepUsecase.IsPossibleToSleep(player); 
                    UpdateOutlineColor(canInteract);
                    outline.enabled = true;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void OnHoverStay<T>(IInteractor<T> interactor) where T : BaseModel
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    bool canInteract = _playerPlayerSleepUsecase.IsPossibleToSleep(player);
                    UpdateOutlineColor(canInteract);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void OnHoverExit<T>(IInteractor<T> interactor) where T : BaseModel
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    outline.enabled = false;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void Interact<T>(IInteractor<T> interactor) where T : BaseModel
        {
            switch (interactor.GetModel())
            {
                case Player player:
                    _playerPlayerSleepUsecase.TrySleep(player);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public string GetDescription()
        {
            return description;
        }

        private void UpdateOutlineColor(bool canInteract)
        {
            outline.OutlineColor = canInteract ? canInteractColor : cannotInteractColor;
        }

        private void OnValidate()
        {
            if (outline == null) {
                outline = GetComponent<Outline>();
                outline.OutlineColor = canInteractColor;
            }
        }
    }
}