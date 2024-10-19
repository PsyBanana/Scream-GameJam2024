using UnityEngine;
using UHFPS.Runtime;

public class ComputerInteractable : MonoBehaviour, IHoverStart, IInteractStart, IHoverEnd
{
    [SerializeField] Transform _cameraTarget;

    public void HoverStart()
    {
        
    }

    public void InteractStart()
    {
        if (!ConnectionManager.Instance.IsConnected)
        {
            // Connect to the computer
            Player3D.Instance.FreezePlayer(true);
            ConnectionManager.Instance.SetConnectionStatus(true);
            PlayerPresenceManager.Instance.SwitchActiveCamera(_cameraTarget.gameObject, 3f, null);
        }
        else
        {
            Player3D.Instance.FreezePlayer(false);
            ConnectionManager.Instance.SetConnectionStatus(false);
            PlayerPresenceManager.Instance.SwitchToPlayerCamera(3f, null);
        }
    }

    public void HoverEnd()
    {
        
    }
}
