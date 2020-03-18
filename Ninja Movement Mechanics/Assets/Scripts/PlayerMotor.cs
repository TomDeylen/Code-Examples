using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : Controller
{
    float delta;
    Inputs inp = new Inputs();

    bool inAction; // Action move - Stop other inputs
    bool grappleClimb; // from grapple to climb mode

    Rigidbody rb;
    Collider col; //Object collider
    [HideInInspector] //Not needed in inspector, so hiding
    public GrapplePoint grapplePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    public override void Tick(float d, Inputs inp)
    {
        delta = d; //Get synced time from main controller
        this.inp = inp;
        if(inAction)
        {
            GrappleMove(delta);
        }
        else
        {
            //activate grapple if button is pressed and in grapple zone
            if(inp.grapple && grapplePoint != null)
            {
                //Go to action mode
                inAction = true;
                //Disable rb and collider
                rb.isKinematic = true;
                col.enabled = false;
            }
        }
    }

    public override void FixedTick(float d)
    {
        delta = d;
        if (inAction)
        {

        }
        else
        {
            Vector3 direction = new Vector3(inp.horizontal, 0, 0);
            rb.velocity = direction;
        }
    }

    void GrappleMove(float delta)
    {

        //When reached grapple point - start climbing up
        if (grappleClimb == true)
        {
            //Move from current point to new point, with the speed of 10 units a second
            transform.position = Vector3.MoveTowards(transform.position, grapplePoint.climbPoint.position, 5 * delta);
            //If close enought. leave action, turn rb and collision back on.
            if (Vector3.Distance(transform.position, grapplePoint.climbPoint.position) < .2f)
            {
                inAction = false;
                rb.isKinematic = false;
                col.enabled = true;
            }
        }
        else // Grapple move!
        {
            //Move from current point to new point, with the speed of 10 units a second
            transform.position = Vector3.MoveTowards(transform.position, grapplePoint.grapplePoint.position, 10 * delta);
            //If close enough to the point, move to climb mode
            if (Vector3.Distance(transform.position, grapplePoint.grapplePoint.position) < 1f)
            {
                grappleClimb = true;
            }
        }

    }
}

