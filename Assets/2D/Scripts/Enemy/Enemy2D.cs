using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    public float moveSpeed = 2f;        // Speed at which the enemy moves
    public float detectionRange = 4f;   // How far the enemy can detect the player
    public float stopDistance = 0.5f;   // Distance to stop when close to the player
    public float attackRange = 2.5f;    // Set to 2.5 for attack range
    public float attackCooldown = 1.5f; // Time between attacks
    private Transform player;           // Reference to the player
    private bool playerInRange = false; // Whether the player is in detection range
    private bool isStanding = false;    // If the enemy has stood up (before chasing)
    private bool isDead = false;        // If the enemy is dead
    private bool canAttack = true;      // If the enemy can attack again
    [SerializeField]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Start with the Eating animation
        animator.SetBool("IsEating", true);

        LoadEnemy2DData();

        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        DetectPlayer();

        if (playerInRange && !isDead)
        {
            if (!isStanding)
            {
                // Stop the Eating animation and start the delay before chasing
                animator.SetBool("IsEating", false);
                StartCoroutine(DelayBeforeChase());
            }
            else
            {
                // After standing up, transition to movement or attack
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer <= attackRange && canAttack)
                {
                    StartCoroutine(Attack());
                }
                else if (distanceToPlayer > attackRange)
                {
                    // Stop attacking if the player is out of attack range
                    animator.SetBool("IsAttacking", false);

                    if (distanceToPlayer > stopDistance)
                    {
                        FollowPlayer();  // Move towards the player if out of attack range
                    }
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
        animator.SetBool("IsAttacking", true);

        canAttack = false;

        // Wait until attack is finished
        yield return new WaitForSeconds(3.5f);

        // Stop the attack animation
        animator.SetBool("IsAttacking", false);

        // Wait for cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    // Delay before starting to chase the player
    IEnumerator DelayBeforeChase()
    {
        yield return new WaitForSeconds(4f);  // 1-second delay
        isStanding = true;  // Now the enemy can start moving
    }

    // Function to handle enemy death
    public void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }

    public void SaveEnemy2DData()
    {
        Vector3 enemyPosition = transform.position;
        ES3.Save(_uniqueID + "_Position", enemyPosition);

        bool isDead = this.isDead;
        ES3.Save(_uniqueID + "_IsDead", isDead);
    }

    public void LoadEnemy2DData()
    {
        if (ES3.KeyExists(_uniqueID + "_Position"))
        {
            Vector3 loadedPosition = ES3.Load<Vector3>(_uniqueID + "_Position");
            transform.position = loadedPosition;

            bool isLoadedDead = ES3.Load<bool>(_uniqueID + "_IsDead");
            this.isDead = isLoadedDead;
        }
    }

    private void OnDestroy()
    {
        SaveEnemy2DData();
    }
}