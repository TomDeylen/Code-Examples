using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, iDamage
{
    bool _isVisible = false;
    public SpriteRenderer rend;
    Vector3 direction;

    public ParticleSystem explosion;
    void Update()
    {
        if (_isVisible == false)
        {
            if (IsVisibleFrom(rend, Camera.main))
            {
                _isVisible = true;
                
                Transform ship = FindObjectOfType<ShipMove>().transform;
                Vector3 shipDirection = -transform.right * 20;
                if (ship == null)
                    shipDirection = ship.position;

                direction = (transform.position - shipDirection).normalized;
            }


            return;
        }
            

        transform.position += -direction * 5 * Time.deltaTime;
    }

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public void Damage(int damage)
    {
        if (damage < 0)
            Death();
    }
    public void Death()
    {
        if (explosion != null)
        { 
            Transform tr = GameObject.FindGameObjectWithTag("Obstacles").transform;

            Instantiate(explosion, transform.position, transform.rotation).transform.parent = tr;
        }
        gameObject.SetActive(false);

    }
}
