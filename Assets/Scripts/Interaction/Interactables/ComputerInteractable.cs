using UnityEngine;
using ScriptableObjectArchitecture;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _cameraTarget;
    [SerializeField] Vector3GameEvent _onCameraInteracted = default(Vector3GameEvent);

    public void OnFocus(IInteractor interactor)
    {
        
    }

    public void OnInteract(IInteractor interactor)
    {
        if (!ConnectionManager.Instance.IsConnected)
        {
            Player3D.Instance.TogglePlayer(false);
            ConnectionManager.Instance.SetConnectionStatus(true); // Connect to the computer
            _onCameraInteracted.Raise(_cameraTarget.position);
        }
        else
        {
            Player3D.Instance.TogglePlayer(true);
            ConnectionManager.Instance.SetConnectionStatus(false);
            _onCameraInteracted.Raise(ConnectionManager.Instance.PlayerMovement3D.transform.position);
        }
    }

    public void OnUnfocus(IInteractor interactor)
    {
        
    }
}
