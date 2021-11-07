using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour {

    public Transform[] cannons;
    public Rigidbody2D bulletPrefab;
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            Fire();
        }
		
    }

    void Fire()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            Instantiate(bulletPrefab, cannons[i].position, cannons[i].rotation);
        }

    }
}
