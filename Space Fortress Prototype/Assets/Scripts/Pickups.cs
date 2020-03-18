using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public ParticleSystem pickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShipMove ship = collision.transform.GetComponent<ShipMove>();
        if(ship)
        {
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
