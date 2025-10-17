using Domain.Models.Common;

namespace Presentation.Common
{
    public interface IInteractable
    {
        void OnHoverEnter<T>(IInteractor<T> interactor) where T : BaseModel;
        void OnHoverExit<T>(IInteractor<T> interactor) where T : BaseModel;
        void Interact<T>(IInteractor<T> interactor) where T : BaseModel;
        string GetDescription();
    }
}