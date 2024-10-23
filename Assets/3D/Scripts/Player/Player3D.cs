using StimpakEssentials;
using UHFPS.Runtime;
using UnityEngine;

public class Player3D : SingletonBehaviour<Player3D>
{
    public bool CanMove { get; set; }

    public void FreezePlayer(bool toggle)
    {
        PlayerPresenceManager.Instance.FreezeMovement(toggle);
    }

    public void TeleportPlayerTo(Vector3 position)
    {
        transform.position = position;
    }
}
