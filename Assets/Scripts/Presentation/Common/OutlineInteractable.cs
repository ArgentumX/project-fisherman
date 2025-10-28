using Domain.Models.Common;
using Domain.Models.Entities.Player;
using UnityEngine;

namespace Presentation.Common
{
    public abstract class OutlineInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string description = "Interact";
        [SerializeField] private Outline outline;
        [SerializeField] private Color canInteractColor = Color.yellowGreen;
        [SerializeField] private Color cannotInteractColor = Color.coral;
        [SerializeField, Min(0)] private float interactionTime = 0;
        
        public void OnHoverEnter<T>(IInteractor<T> interactor) where T : BaseModel
        {
            bool canInteract = CanInteract(interactor); 
            UpdateOutlineColor(canInteract);
            outline.enabled = true;
        }

        public void OnHoverStay<T>(IInteractor<T> interactor) where T : BaseModel
        {
            bool canInteract = CanInteract(interactor); 
            UpdateOutlineColor(canInteract);
        }

        public void OnHoverExit<T>(IInteractor<T> interactor) where T : BaseModel
        {
            outline.enabled = false;
        }
        public abstract void Interact<T>(IInteractor<T> interactor) where T : BaseModel;

        public abstract bool CanInteract<T>(IInteractor<T> interactor) where T : BaseModel;

        public virtual float GetInteractionTime() => interactionTime;
        public virtual string GetDescription() {
            return description;
        }
        private void UpdateOutlineColor(bool canInteract) {
            outline.OutlineColor = canInteract ? canInteractColor : cannotInteractColor;
        }
        
        protected virtual void ResetBase()
        {
            if (outline == null) {
                outline = GetComponent<Outline>();
                outline.OutlineColor = canInteractColor;
            }
        }
    }
}