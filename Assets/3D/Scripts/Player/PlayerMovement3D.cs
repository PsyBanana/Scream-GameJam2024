using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{

    [Header("Movement")]
    public float MovementSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public bool canMove = true; // if the connection is made player should not move.


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right *horizontalInput;

        rb.AddForce(moveDirection.normalized * MovementSpeed * 10f, ForceMode.Force);
    }
}
