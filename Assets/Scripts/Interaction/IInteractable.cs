public interface IInteractable
{
    void OnInteract(IInteractor interactor);
    void OnFocus(IInteractor interactor);
    void OnUnfocus(IInteractor interactor);
}
