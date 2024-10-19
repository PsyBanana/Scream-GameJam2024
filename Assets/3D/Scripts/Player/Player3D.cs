using StimpakEssentials;
using UHFPS.Runtime;

public class Player3D : SingletonBehaviour<Player3D>
{
    public bool CanMove { get; set; }

    public void FreezePlayer(bool toggle)
    {
        PlayerPresenceManager.Instance.FreezeMovement(toggle);
    }
}
