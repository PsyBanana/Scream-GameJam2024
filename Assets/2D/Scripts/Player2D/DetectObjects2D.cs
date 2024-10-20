using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects2D : MonoBehaviour
{
    public float rayDistance = 3f;
    private PlayerMovement2D playerMovement;  // Reference to PlayerMovement2D
    private bool canAttack = true;

    void Start()
    {
        // Get the PlayerMovement2D component from the player
        playerMovement = GetComponent<PlayerMovement2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Left mouse button
        {
            canAttack = false;
            ShootRay();
            StartCoroutine(ReloadAttack());
        }
    }

    void ShootRay()
    {
        Vector3 direction = Vector3.zero;

        // Determine the direction to shoot based on the player's last direction
        switch (playerMovement.LastDirection)
        {
            case 0: // Up
                direction = Vector3.up;
                break;
            case 1: // Down
                direction = Vector3.down;
                break;
            case 2: // Right
                direction = Vector3.right;
                break;
            case 3: // Left
                direction = Vector3.left;
                break;
        }

        Vector3 origin = transform.position;

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

    private IEnumerator ReloadAttack()
    {
        yield return  new WaitForSeconds(1f);
        canAttack = true;
    }
}