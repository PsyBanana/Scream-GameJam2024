using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    public Camera PlayerCamera; // Assign the 3D camera in the Inspector

    void Start()
    {
        if (PlayerCamera == null)
        {
            PlayerCamera = Camera.main; // Default to the main camera if not set
        }
    }

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Get the position of the mouse in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Set the z-coordinate to the distance between the player and the camera
        mouseScreenPosition.z = Mathf.Abs(PlayerCamera.transform.position.z - transform.position.z);

        // Convert the screen position to world space
        Vector3 mouseWorldPosition = PlayerCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mouseWorldPosition - transform.position;

        // Set the z-axis to 0 to ensure the rotation is only on the 2D plane
        direction.z = 0f;

        // Calculate the angle and apply the rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}