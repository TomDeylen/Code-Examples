using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float ufSpeed = 6;
    public float speed = 2;

    Rigidbody2D rb;
    float sideInput;
    float upInput;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sideInput = Input.GetAxis("Horizontal");
        upInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            FocusedMove();
        else if (!Input.GetKey(KeyCode.LeftShift))
            UnfocusedMove();

        //if ()
    }



    private void FocusedMove()
    {
        Vector2 vel = rb.velocity;
        vel.x = sideInput * speed;
        rb.velocity = vel;

        Vector2 uvel = rb.velocity;
        uvel.y = upInput * speed;
        rb.velocity = uvel;

    }

    private void UnfocusedMove()
    {
        Vector2 vel = rb.velocity;
        vel.x = sideInput * ufSpeed;
        rb.velocity = vel;

        Vector2 uvel = rb.velocity;
        uvel.y = upInput * ufSpeed;
        rb.velocity = uvel;

    }
}


