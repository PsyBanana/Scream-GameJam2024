using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;

    public Animator animator;

    Vector2 movement;

    public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    void FixedUpdate()
    {
        
        if (canMove)
        {
            
            Vector3 moveVector = new Vector3(movement.x, movement.y, 0);

           
            rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Immediate response
        float moveY = Input.GetAxisRaw("Vertical");

       
        movement = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", movement.sqrMagnitude); 


        // Determine last direction based on movement
        if (movement != Vector2.zero)
        {
            if (moveY > 0)
            {
                animator.SetInteger("LastDirection", 0); // Up
            }
            else if (moveY < 0)
            {
                animator.SetInteger("LastDirection", 1); // Down
            }
            else if (moveX > 0)
            {
                animator.SetInteger("LastDirection", 2); // Right
            }
            else if (moveX < 0)
            {
                animator.SetInteger("LastDirection", 3); // Left
            }
        }
    }
}