using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    Camera cam;
    bool onScreen;
    public Rigidbody2D EnemyPrefab;
    public Transform SpawnPoint;
	SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }
	
	public bool onScreenFrom(Renderer renderer, Camera camera)
	{
	    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
	    return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}


    // Update is called once per frame
    void Update()
    {
        if (onScreenFrom(rend, cam) && onScreen == false)
        {
            Debug.Log("Visible");
            onScreen = true;
        }

        //Be enemy

        //Spawn enemies here.

        if (onScreen == true)
        {
            Instantiate(EnemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
			onScreen = false;
            gameObject.SetActive(false);
        }

    }
	

}
