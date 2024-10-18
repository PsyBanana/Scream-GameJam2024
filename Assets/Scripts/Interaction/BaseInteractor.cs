using UnityEngine;

public abstract class BaseInteractor : MonoBehaviour, IInteractor
{
    public bool CanInteract;

    public abstract void DetectInteractable();

    public abstract void Interact();
}
