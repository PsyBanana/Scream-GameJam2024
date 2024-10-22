using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2D : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; 
    public Vector2 offset;

    void Start()
    {
        // Find the Player2D GameObject and assign its Transform
        GameObject playerObject = GameObject.Find("Player2D");
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's Transform
        }
        else
        {
            Debug.LogWarning("Player2D not found in the scene.");
        }
    }

    void LateUpdate()
    {
        if (player != null) // Check if player is assigned
        {
            Vector2 desiredPosition = (Vector2)player.position + offset;
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}