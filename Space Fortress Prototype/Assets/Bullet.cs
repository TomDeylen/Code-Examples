using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explosion;
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        iDamage damagable = collision.transform.GetComponent<iDamage>();
        if(damagable != null)
        {
            damagable.Damage(-1);
        }

        if (explosion != null)
        {
            explosion.transform.parent = null;
            explosion.Play();
        }
        Destroy(gameObject);
    }

}
