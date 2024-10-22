using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public float moveSpeed = 2f;        // Speed at which the enemy moves
    public float detectionRange = 4f;   // How far the enemy can detect the player
    public float stopDistance = 0.5f;   // Distance to stop when close to the player
    public float attackRange = 1f;      // Range within which the enemy can attack
    public float attackCooldown = 1.5f; // Time between attacks
    private Transform player;           // Reference to the player
    private bool playerInRange = false; // Whether the player is in detection range
    private bool isStanding = false;    // If the enemy has stood up (before chasing)
    private bool isDead = false;        // If the enemy is dead
    private bool canAttack = true;      // If the enemy can attack again
    [SerializeField]
    private Animator animator;          // Reference to the animator

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();
        if (playerInRange && !isDead)
        {
            if (!isStanding)
            {
                
                animator.SetBool("isStanding", true);
                isStanding = true;
            }
            else
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer <= attackRange && canAttack)
                {
                    
                    StartCoroutine(Attack());
                }
                else if (distanceToPlayer > stopDistance)
                {
                    FollowPlayer();
                }
            }
        }
    }

    // Detect if the player is within the enemy's range
    void DetectPlayer()
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

    // Move the enemy towards the player if they are within range, but stop at a certain distance
    void FollowPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

         
            if (distanceToPlayer > stopDistance)
            {
            
                Vector3 direction = (player.position - transform.position).normalized;

                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);

               
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    // Coroutine to handle attacking the player
    IEnumerator Attack()
    {
        // Trigger the attack animation
        animator.SetBool("isAttacking", true);

        canAttack = false;

       
        yield return new WaitForSeconds(0.5f);

       
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true; 
    }

    // Function to handle enemy death
    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true); 
    }
}