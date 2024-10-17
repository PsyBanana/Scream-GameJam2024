using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{

    public float speed = 10f;

    private Rigidbody2D rb;

    public bool canMove = false;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

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

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Use GetAxisRaw for immediate response
        float moveY = Input.GetAxisRaw("Vertical");

        // Create the movement vector
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Set the velocity directly based on input
        rb.velocity = moveDirection * speed;

    }
}
