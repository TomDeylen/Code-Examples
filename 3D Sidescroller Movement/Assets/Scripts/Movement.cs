using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody rb;
    private CapsuleCollider capColl;
    //Temp 
    public GameObject characterCapsuel;

    [Header("Stats")]
    public float walkSpeed = 5;
    public float duckSpeed = 2;
    float speed = 10;

    public float jumpForce = 50;
    public float sideSpeed = 2;
    public float wallJumpLerp = 10;

    public bool canMove;
    public bool wallSlide;
    public bool wallJumped;
    public bool ducking;

    public int side = 1;

    void Start()
    {
        coll = GetComponent<Collision>();
        capColl = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        speed = walkSpeed;
        canMove = true;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        Walk(dir);

        if(Input.GetButtonDown("Jump"))
        {
            if(coll.onGround)
                Jump(Vector3.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if(coll.onWall && !coll.onGround)
        {
            if(x != 0)
            {
                wallSlide = true;
                WallSlide();
            }

        }

        if(Input.GetButtonDown("Fire1"))
        {
            ducking = true;
            Duck();
        }
        if ((Input.GetButtonUp("Fire1") || (Input.GetButtonDown("Jump") && coll.onGround)) && coll.hitCeiling == false)
        {
            ducking = false;
            Duck();
        }
        else if(Input.GetButton("Fire1") == false && coll.hitCeiling == false)
        {
            ducking = false;
            Duck();
        }

        if (coll.onGround)
        {
            wallJumped = false;
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (wallSlide || !canMove)
            return;

        if (x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1;
        }
    }

    void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        if(!wallJumped)
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        else
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
    }

    void Jump (Vector3 dir, bool wall)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0);
        rb.velocity += (dir * jumpForce);
    }

    void WallSlide()
    {
        //if(coll.wallSide != side)
        if (!canMove)
            return;

        bool pushingWall = false;

        if((rb.velocity.x >0 && coll.onRightWall) || (rb.velocity.x <0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;
        rb.velocity = new Vector2(push, -sideSpeed);
    }

    void Duck()
    {
        if (ducking == true)
        {
            speed = duckSpeed;
            coll.Duck(ducking);
            capColl.height = 1;
            capColl.center = new Vector3(0, .5f, 0);
            characterCapsuel.transform.localPosition = new Vector3(0, .5f, 0);
            characterCapsuel.transform.localScale = new Vector3(1, .5f, 1);
        }
        else
        {
            speed = walkSpeed;
            coll.Duck(ducking);
            capColl.height = 2;
            capColl.center = new Vector3(0, 1f, 0);
            characterCapsuel.transform.localPosition = new Vector3(0, 1f, 0);
            characterCapsuel.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void GroundSlide()
    {

    }

    void WallJump()
    {
        if((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector3 wallDir = coll.onRightWall ? Vector3.left : Vector3.right;
        Jump(((Vector3.up / 1.5f) + (wallDir)), true);

        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

}
