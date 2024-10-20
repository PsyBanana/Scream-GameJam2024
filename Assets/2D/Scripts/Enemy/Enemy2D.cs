using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public float moveSpeed = 2f;        
    public float detectionRange = 4f;
    [SerializeField]
    private Transform player;           
    private bool playerInRange = false; 
    public float stopDistance = 0.5f;

    void Update()
    {
        DetectPlayer();
        if (playerInRange)
        {
            FollowPlayer();
        }
    }

  
    void DetectPlayer()  // Detect if the player is within the enemy's range
    {
       
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player2D"); 
            if (playerObject != null)
            {
                player = playerObject.transform; 
            }
        }

       
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            playerInRange = distance <= detectionRange;
        }
    }

    
    void FollowPlayer()  // Move the enemy towards the player if they are within range, but stop at a certain distance
    {
        if (player != null)
        {
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            
            if (distanceToPlayer > stopDistance)
            {
                
                Vector3 direction = (player.position - transform.position).normalized;

                
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }
}