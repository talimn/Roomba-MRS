using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMovement;
    private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveLeft = false;
        moveRight = false;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Movements()
    {
        if (moveLeft)
        {
            horizontalMovement = -speed;
        }

        else if (moveRight)
        {
            horizontalMovement = speed;
        }

        else
        {
            horizontalMovement = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontalMovement, rb.position.y, rb.position.z); 
    }
}
