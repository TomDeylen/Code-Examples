using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor2D : MonoBehaviour
{
    Rigidbody2D rb;

    float forwardInput;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerController.Instance.Set2DMotor(this);
    }

    public void GetInputs(float Horizontal)
    {
        forwardInput = Horizontal;
    }

    public void Tick(float delta)
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = Vector3.right * forwardInput * 5;
        rb.velocity = direction;
    }

}
