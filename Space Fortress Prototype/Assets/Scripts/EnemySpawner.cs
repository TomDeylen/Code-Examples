using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public EnemyMovement enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelTrigger")
        {
            Spawn();
        }
        
    }

    void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemy, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

}
