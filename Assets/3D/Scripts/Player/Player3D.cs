using StimpakEssentials;
using System.Collections;
using UHFPS.Runtime;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Player3D : SingletonBehaviour<Player3D>
{
    [SerializeField] GameEvent _onPlayer3DBlink = default(GameEvent);

    public bool CanMove { get; set; }

    public void FreezePlayer(bool toggle)
    {
        PlayerPresenceManager.Instance.FreezeMovement(toggle);
    }

    public void TeleportPlayerTo(Vector3 position)
    {
        transform.position = position;
    }

    public void BlinkEyes()
    {
        StartCoroutine(BlinkEyesCO());
    }

    IEnumerator BlinkEyesCO()
    {
        UIManager.Get<UIFade>().Fade(0f, 1f, 0.2f);
        _onPlayer3DBlink.Raise();
        yield return new WaitForSeconds(0.2f);
        UIManager.Get<UIFade>().Fade(1f, 0f, 0.2f);
    }
}
