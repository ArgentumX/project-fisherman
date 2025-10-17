using Application.Interfaces.Usecases;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Presentation.Common;
using UnityEngine;
using Zenject;

namespace Presentation.Sleep
{
    [RequireComponent(typeof(Outline))]
    public class BedView : MonoBehaviour, IInteractable
    {
        [SerializeField] private string description = "Спать";
        [SerializeField] private Outline outline;
        private IPlayerSleepUsecase _playerPlayerSleepUsecase;
            
        [Inject]
        private void Construct(IPlayerSleepUsecase playerPlayerSleepUsecase)
        {
            _playerPlayerSleepUsecase = playerPlayerSleepUsecase;
        }
        // TODO красный цвет выделения если невозможно взаимодействовать
        public void OnHoverEnter<T>(IInteractor<T> interactor) where T : BaseModel
        {
            outline.enabled = true;
        }

        public void OnHoverExit<T>(IInteractor<T> interactor) where T : BaseModel
        {
            outline.enabled = false;
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

        private void OnValidate()
        {
            if (outline == null) {
                outline = GetComponent<Outline>();
            }
        }
    }
}