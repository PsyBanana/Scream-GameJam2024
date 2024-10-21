using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2D : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; 
    public Vector2 offset;

    void LateUpdate()
    {
        
        Vector2 desiredPosition = (Vector2)player.position + offset;

        
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}