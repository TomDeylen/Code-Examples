using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float MovementSpeed = 2.0f;
    public float JumpSpeed = 4f;
    public float groundRadiusCheck = 0.3f;
    public LayerMask layers;
    bool faceRight = false;
    SpriteRenderer characterSprite;
   
    Rigidbody2D rb;
    float moveInput;
    bool jumpInput = false;
    


    void Update () {
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButton("Jump");

        if (moveInput < 0) // Moving Left <-
            faceRight = false;
        else if (moveInput > 0) // Moving Right ->
            faceRight = true;
        characterSprite.flipX = faceRight;
    }


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        vel.x = moveInput * MovementSpeed;
        rb.velocity = vel;
        bool onGround = GroundCheck();
        if(jumpInput == true && onGround == true)
        {
            vel.y = JumpSpeed;
        }
        rb.velocity = vel;
    }

    bool GroundCheck()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(
                            transform.position,
                            groundRadiusCheck,
                            layers);
        return hitCollider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, groundRadiusCheck);
    }

    
}
