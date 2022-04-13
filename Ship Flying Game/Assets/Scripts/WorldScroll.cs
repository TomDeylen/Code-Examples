using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroll : MonoBehaviour
{

    public float xDir;
    public float yDir = 5;

    public Transform[] terrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Scroll = new Vector3(xDir, yDir, 0) *Time.deltaTime;
        for (int i = 0; i < terrain.Length; i++)
        {
            terrain[i].position += Scroll;
        }
    }
}
