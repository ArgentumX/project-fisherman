namespace Presentation.Common
{
    public interface IInteractable
    {
        void OnHoverEnter();
        void OnHoverExit();
        void Interact();
        string GetDescription();
    }
}