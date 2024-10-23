using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;

    public Animator animator;
    public Animator ShadowAnimation; // Reference to the shadow's animator

    Vector2 movement;

    public bool canMove = false;

    public bool isAxeEquipped = false;


    // Store the last direction (0 = Up, 1 = Down, 2 = Right, 3 = Left)
    public int LastDirection { get; set; }

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
        
        if (canMove )
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
        {
            // Determine last direction
            if (moveY > 0)
            {
                LastDirection = 0; // Up
            }
            else if (moveY < 0)
            {
                LastDirection = 1; // Down
            }
            else if (moveX > 0)
            {
                LastDirection = 2; // Right
            }
            else if (moveX < 0)
            {
                LastDirection = 3; // Left
            }
            animator.SetInteger("LastDirection", LastDirection);
        }
    }

    public void EquipAxe(bool equip)
    {
        isAxeEquipped = equip; // Set the equipped state
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe")) // Assuming your axe has the tag "Axe"
        {
            EquipAxe(true); // Equip the axe
            Destroy(other.gameObject); // Destroy the axe object
        }
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}