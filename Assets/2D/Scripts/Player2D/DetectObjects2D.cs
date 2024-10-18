using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects2D : MonoBehaviour
{
    public float rayDistance = 3f; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            ShootRay();
        }
    }
    // For now the Player Destroys the Object - should make the tree fall.
    void ShootRay()
    {
        
        Vector3 origin = transform.position;

        
        Vector3 direction = transform.right; 

       
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name); 

            ObjectLife objectLife = hit.collider.GetComponent<ObjectLife>();
            if (objectLife != null)
            {
                objectLife.TakeDamage(1); 
            }
        }
        else
        {
            Debug.Log("No hit detected");
        }

 
        Debug.DrawRay(origin, direction * rayDistance, Color.red, 3f); 
    }
}