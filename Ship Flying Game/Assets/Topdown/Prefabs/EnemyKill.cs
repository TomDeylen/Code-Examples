using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
	public int Health = 100;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * 4;
        Debug.Log(rb.velocity);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PlayerBullet")
        {
            Debug.Log ("hit");
			Health = Health-10;
        }
		
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            Health -= Health;
        }
            


		if (Health <= 0)
		{
			Destroy(gameObject);
		}
    }
}
