using UnityEngine;

public class ComputerNotInteractable : MonoBehaviour
{
    Transform _playerCameraTransform;

    private void Awake()
    {
        _playerCameraTransform = Camera.main.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player3D player3D = other.gameObject.GetComponent<Player3D>();

            if (player3D != null)
            {
                Vector3 directionToTarget = (transform.position - _playerCameraTransform.position).normalized;
                float dotProduct = Vector3.Dot(_playerCameraTransform.forward, directionToTarget);

                if (dotProduct >= 0.75f)
                {
                    player3D.BlinkEyes();
                }
            }
        }
    }
}
