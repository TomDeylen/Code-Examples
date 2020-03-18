using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public Transform grapplePoint;
    public Transform climbPoint;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMotor player = other.GetComponent<PlayerMotor>();
        if (player != null)
        {
            player.grapplePoint = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //When player exits, clear grapple points
        PlayerMotor player = other.GetComponent<PlayerMotor>();
        if (player != null)
        {
            player.grapplePoint = null;
        }
    }
}
