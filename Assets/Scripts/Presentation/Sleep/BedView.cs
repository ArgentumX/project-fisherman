using Application.Interfaces.Usecases;
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
        private ISleepUsecase _playerSleepUsecase;
            
        [Inject]
        private void Constructor(ISleepUsecase playerSleepUsecase)
        {
            _playerSleepUsecase = playerSleepUsecase;
        }
        public void OnHoverEnter()
        {
            outline.enabled = true;
        }

        public void OnHoverExit()
        {
            outline.enabled = false;
        }

        public void Interact()
        {
            _playerSleepUsecase.TrySleep();
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