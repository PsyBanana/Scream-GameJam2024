using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{

    public Transform cameraPosition;

    
    void Update()
    {
        if (ConnectionManager.Instance.IsConnected)
        {
            return;
        }

        transform.position = cameraPosition.position;
    }
}
