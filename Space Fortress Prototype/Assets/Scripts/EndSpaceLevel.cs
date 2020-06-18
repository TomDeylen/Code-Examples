using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSpaceLevel : MonoBehaviour
{
    public ShipMove shipMove;
    public MovePoints[] movePoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LevelTrigger")
        {
            shipMove.ActivateAutoMove(movePoints);
        }
    }

}

[System.Serializable]
public class MovePoints
{
    public Transform point;
    public float speed;
}
