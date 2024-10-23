using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects2D : MonoBehaviour
{
    public float rayDistance = 3f;
    private PlayerMovement2D playerMovement;
    private bool canAttack = true;

    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement2D>();
    }

    void Update()
    {
        // Only allow attacking if the axe is equipped
        if (Input.GetMouseButtonDown(0) && canAttack && playerMovement.isAxeEquipped)
        {
            canAttack = false; 
            ShootRay(); 
            StartCoroutine(ReloadAttack()); 
        }
    }

    void ShootRay()
    {
        playerMovement.animator.SetTrigger("Attack"); // Trigger attack animation
        Debug.Log("PlayerAttack");

        Vector3 direction = Vector3.zero;

        // Determine the direction to shoot based on the player's last direction
        switch (playerMovement.LastDirection)
        {
            case 0: 
                direction = Vector3.up;
                break;
            case 1: 
                direction = Vector3.down;
                break;
            case 2: 
                direction = Vector3.right;
                break;
            case 3: 
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

        Debug.DrawRay(origin, direction * rayDistance, Color.red, 3f); // Visualize the ray
    }

    private IEnumerator ReloadAttack()
    {
        yield return new WaitForSeconds(1f); 
        canAttack = true; 
    }
}