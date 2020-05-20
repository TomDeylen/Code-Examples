using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public LayerMask groundLayer;
    public bool hitCeiling;
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;


    CapsuleCollider capsule;
    public float collisionRadius = 0.1f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    public Vector3 boxSize;
    float capStandHeight;
    Vector2 currentBottomOffset, currentRightOffset, currentLeftOffset;
    Vector3 currentBoxSize;


    private void Start()
    {
        currentBottomOffset = bottomOffset;
        currentRightOffset = rightOffset;
        currentLeftOffset = leftOffset;
        currentBoxSize = boxSize;
        capsule = GetComponent<CapsuleCollider>();
        capStandHeight = capsule.height;
    }

    void Update()
    {
        onGround = Physics.OverlapSphere(transform.position + (Vector3)currentBottomOffset, collisionRadius, groundLayer).Length > 0;
        //onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        hitCeiling = Physics.OverlapSphere(transform.position + (Vector3.up * capStandHeight), collisionRadius, groundLayer).Length > 0;

        onWall = Physics.OverlapBox(transform.position + (Vector3)currentRightOffset, (Vector3)currentBoxSize/2, Quaternion.identity, groundLayer).Length > 0
            || Physics.OverlapBox(transform.position + (Vector3)currentLeftOffset, (Vector3)currentBoxSize/2, Quaternion.identity, groundLayer).Length > 0;


        //onWall = Physics.OverlapSphere(transform.position + (Vector3)rightOffset, collisionRadius, groundLayer).Length > 0
        //    || Physics.OverlapSphere(transform.position + (Vector3)leftOffset, collisionRadius, groundLayer).Length > 0;
        //onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
        //    || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics.OverlapBox(transform.position + (Vector3)currentRightOffset, (Vector3)currentBoxSize/2, Quaternion.identity, groundLayer).Length > 0;
        onLeftWall = Physics.OverlapBox(transform.position + (Vector3)currentLeftOffset, (Vector3)currentBoxSize/2, Quaternion.identity, groundLayer).Length > 0;
        //onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        //onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    public void Duck(bool ducking)
    {
        if(ducking == true)
        {
            currentRightOffset = new Vector2(rightOffset.x, rightOffset.y / 2);
            currentLeftOffset = new Vector2(leftOffset.x, leftOffset.y / 2);
            currentBoxSize = new Vector3 (boxSize.x, boxSize.y / 2, boxSize.z);
        }
        else
        {
            currentRightOffset = rightOffset;
            currentLeftOffset = leftOffset;
            currentBoxSize = boxSize;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + (Vector3)currentBottomOffset, collisionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3.up * capStandHeight), collisionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3)currentLeftOffset, (Vector3)currentBoxSize);
        Gizmos.DrawWireCube(transform.position + (Vector3)currentRightOffset, (Vector3)currentBoxSize);
    }
}
